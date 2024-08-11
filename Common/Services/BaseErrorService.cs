using Common.Models;
using System;

namespace Common.Services
{
    public class BaseErrorService
    {
        private readonly BaseDapperContext dapperContext;

        public BaseErrorService(string connection)
        {
            this.dapperContext = new BaseDapperContext(connection);
        }
        /// <summary>
        /// if you have InsertErrorLog sp with these parameter then just call saveError
        /// Otherwise override this method 
        /// </summary>
        /// <param name="exception"></param>
        public virtual void SaveError(Exception exception)
        {
            var ds = new BaseExceptionLog
            {
                Message = exception.Message.ToString(),
                Source = exception.Source.ToString(),
                StackTrace = exception.StackTrace.ToString(),
                Date = DateTime.Now.ToString()
            };
            dapperContext.Execute("Exec InsertErrorLog @Message, @Source, @StackTrace, @Date", ds);
        }

    }
}
