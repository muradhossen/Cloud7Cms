using Common.Models;
using Dapper;
using System.Data;
using System.Data.SqlClient;
public class BaseDapperContext : IDisposable
{
    private readonly IDbConnection _dbConnection;
    //public readonly bool IsDbConnectionReady;
    public BaseDapperContext(string connectionString)
    {
        _dbConnection = new SqlConnection(connectionString);
        //IsDbConnectionReady = !string.IsNullOrEmpty(_dbConnection.ConnectionString);
    }
    public bool IsConnectionOpen()
    {
        try
        {
            if (_dbConnection.State != ConnectionState.Open)
                _dbConnection.Open();
        }
        catch (Exception ex)
        {
            return false;
        }
        return _dbConnection.State == ConnectionState.Open;
    }
    public void Open()
    {
        if (_dbConnection.State != ConnectionState.Open)
            _dbConnection.Open();
    }

    public void Close()
    {
        if (_dbConnection.State != ConnectionState.Closed)
            _dbConnection.Close();
    }
    public TEntity QueryFirstOrDefault<TEntity>(string sql, object parameters = null)
    {
        Open();
        return _dbConnection.QueryFirstOrDefault<TEntity>(sql, parameters);
    }
    public IEnumerable<TEntity> Query<TEntity>(string sql, object parameters = null)
    {
        Open();
        return _dbConnection.Query<TEntity>(sql, parameters);
    }
    public int Execute(string sql, object parameters = null, CommandType commandType = CommandType.Text, int? commandTimeout = null)
    {
        Open();
        return _dbConnection.Execute(sql, parameters, commandType: commandType, commandTimeout: commandTimeout);
    }
    public async Task<int> ExecuteAsync(string sql, object parameters = null, CommandType commandType = CommandType.Text, int? commandTimeout = null)
    {
        Open();
        return await _dbConnection.ExecuteAsync(sql, parameters, commandType: commandType, commandTimeout: commandTimeout);
    }
    /// <summary>
    /// use this mehtod if requirement goes with this method body
    /// otherwise override it
    /// </summary>
    /// <param name="exception"></param>
    public virtual void SaveError(Exception exception, string msg = null)
    {
        try
        {
            _dbConnection.Execute("Exec spInsertError @Message, @Source, @StackTrace, @Date", new ExceptionLog
            {
                Message = msg != null ? exception.Message.ToString() + "::" + msg : exception.Message.ToString(),
                Source = exception.Source?.ToString(),
                StackTrace = exception.StackTrace?.ToString(),
                Date = DateTime.Now.ToString()
            });
        }
        catch (Exception e)
        {

        }
    }
    public virtual void SaveKafkaEventFailedLog(Exception exception, string msisdn, string serviceId, string transactionId, string msg = null)
    {
        try
        {
            _dbConnection.Execute(@"Exec [spInsertKafkaEventFailedLog] @Message, @Source, @StackTrace, @Date, 
                @serviceId, @msisdn, @transactionId", new 
            {
                Message = msg != null ? exception.Message.ToString() + "::" + msg : exception.Message.ToString(),
                Source = exception.Source?.ToString(),
                StackTrace = exception.StackTrace?.ToString(),
                Date = DateTime.Now.ToString(),
                serviceId = serviceId,
                msisdn = msisdn,
                transactionId = transactionId
            });
        }
        catch (Exception e)
        {

        }
    }
    public void Dispose()
    {
        _dbConnection.Dispose();
    }
}
