using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using ImpactTrace.Web.Data;
using ImpactTrace.Web.Models;
using Microsoft.EntityFrameworkCore;

namespace ImpactTrace.Web.Services
{
    public interface ISqlInterceptorService
    {
        void InterceptSql(string sqlText);
        int? GetCurrentRecordingId();
        void SetCurrentRecordingId(int? recordingId);
    }

    public class SqlInterceptorService : ISqlInterceptorService
    {
        private readonly IServiceProvider _serviceProvider;
        private int? _currentRecordingId;

        public SqlInterceptorService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public void SetCurrentRecordingId(int? recordingId)
        {
            _currentRecordingId = recordingId;
        }

        public int? GetCurrentRecordingId()
        {
            return _currentRecordingId;
        }

        public void InterceptSql(string sqlText)
        {
            if (_currentRecordingId == null || string.IsNullOrWhiteSpace(sqlText))
                return;

            var operationType = DetermineOperationType(sqlText);
            if (operationType == null)
                return; // Not an INSERT, UPDATE, or DELETE

            var tableName = ExtractTableName(sqlText, operationType);
            if (string.IsNullOrEmpty(tableName))
                return;

            // Store the SQL operation
            using var scope = _serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            
            var operation = new SqlOperation
            {
                RecordingId = _currentRecordingId.Value,
                TableName = tableName,
                OperationType = operationType,
                SqlText = sqlText,
                ExecutedAt = DateTime.Now
            };

            context.SqlOperations.Add(operation);
            context.SaveChanges();
        }

        private string? DetermineOperationType(string sql)
        {
            var upperSql = sql.TrimStart().ToUpperInvariant();
            
            if (upperSql.StartsWith("INSERT"))
                return "INSERT";
            if (upperSql.StartsWith("UPDATE"))
                return "UPDATE";
            if (upperSql.StartsWith("DELETE"))
                return "DELETE";
            
            return null;
        }

        private string ExtractTableName(string sql, string operationType)
        {
            try
            {
                string pattern = operationType switch
                {
                    "INSERT" => @"INSERT\s+INTO\s+([^\s(]+)",
                    "UPDATE" => @"UPDATE\s+([^\s]+)",
                    "DELETE" => @"DELETE\s+FROM\s+([^\s]+)",
                    _ => string.Empty
                };

                if (string.IsNullOrEmpty(pattern))
                    return string.Empty;

                var match = Regex.Match(sql, pattern, RegexOptions.IgnoreCase);
                if (match.Success && match.Groups.Count > 1)
                {
                    var tableName = match.Groups[1].Value.Trim();
                    // Remove schema prefix if exists (e.g., dbo.TableName -> TableName)
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
}
