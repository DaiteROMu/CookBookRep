using System;
using log4net;
using log4net.Config;

namespace CookBook.Common.Log
{
    public class Logger: ILogger
    {
        private static readonly ILog _log;
        
        static Logger()
        {
            _log = LogManager.GetLogger("Logger");
            XmlConfigurator.Configure();
        }
        
        public void ErrorMessage(Exception ex)
        {
            _log.Error(ex.Message, ex);
        }
    }
}
