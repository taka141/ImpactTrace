namespace ImpactTrace.Core.Application.Interfaces;

/// <summary>
/// SQL Interception service interface
/// </summary>
public interface ISqlInterceptorService
{
    Task InterceptSqlAsync(string sqlText);
}
