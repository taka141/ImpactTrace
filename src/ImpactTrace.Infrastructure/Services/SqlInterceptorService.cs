using System.Text.RegularExpressions;
using ImpactTrace.Core.Application.Interfaces;
using ImpactTrace.Core.Domain.Entities;
using ImpactTrace.Core.Domain.Repositories;
using ImpactTrace.Core.Domain.ValueObjects;

namespace ImpactTrace.Infrastructure.Services;

public class SqlInterceptorService : ISqlInterceptorService
{
    private readonly IRecordingRepository _repository;

    public SqlInterceptorService(IRecordingRepository repository)
    {
        _repository = repository;
    }

    public async Task InterceptSqlAsync(string sqlText)
    {
        if (string.IsNullOrWhiteSpace(sqlText))
            return;

        var activeRecording = await _repository.GetActiveRecordingAsync();
        if (activeRecording == null)
            return;

        var operationType = DetermineOperationType(sqlText);
        if (operationType == null)
            return;

        var tableName = ExtractTableName(sqlText, operationType.Value);
        if (string.IsNullOrEmpty(tableName))
            return;

        var operation = SqlOperation.Create(
            activeRecording.Id,
            new TableName(tableName),
            operationType.Value,
            new SqlText(sqlText)
        );

        activeRecording.AddOperation(operation);
        await _repository.UpdateAsync(activeRecording);
        await _repository.SaveChangesAsync();
    }

    private static OperationType? DetermineOperationType(string sql)
    {
        var upperSql = sql.TrimStart().ToUpperInvariant();

        if (upperSql.StartsWith("INSERT"))
            return OperationType.Insert;
        if (upperSql.StartsWith("UPDATE"))
            return OperationType.Update;
        if (upperSql.StartsWith("DELETE"))
            return OperationType.Delete;

        return null;
    }

    private static string ExtractTableName(string sql, OperationType operationType)
    {
        try
        {
            string pattern = operationType switch
            {
                OperationType.Insert => @"INSERT\s+INTO\s+([^\s(]+)",
                OperationType.Update => @"UPDATE\s+([^\s]+)",
                OperationType.Delete => @"DELETE\s+FROM\s+([^\s]+)",
                _ => string.Empty
            };

            if (string.IsNullOrEmpty(pattern))
                return string.Empty;

            var match = Regex.Match(sql, pattern, RegexOptions.IgnoreCase);
            if (match.Success && match.Groups.Count > 1)
            {
                var tableName = match.Groups[1].Value.Trim();
                // Remove schema prefix if exists
                if (tableName.Contains('.'))
                {
                    tableName = tableName.Split('.').Last();
                }
                // Remove brackets if exists
                tableName = tableName.Trim('[', ']');
                return tableName;
            }
        }
        catch
        {
            // If regex fails, return empty string
        }

        return string.Empty;
    }
}
