using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrintumProjektverwaltung.Helper
{
    public class LogHelper
    {
        private static readonly ILog _debugLogger;
        private static readonly ILog _emailLogger;

        private static ILog GetLogger(string logName)
      {
            ILog log = LogManager.GetLogger(logName);
            return log;
        }
     
       static LogHelper()
       {
           //logger names are mentioned in <log4net> section of config file
           _debugLogger = GetLogger("MyApplicationDebugLog");
            //_emailLogger = GetLogger("EmailLogger");
       }
    
       /// <summary>
       /// This method will write log in Log_USERNAME_date{yyyyMMdd}.log file
       /// </summary>
       /// <param name="message"></param>
       public static void WriteDebugLog(string message)
       {
           _debugLogger.DebugFormat(message);
            //_emailLogger.DebugFormat(message);
       }
   }
}
