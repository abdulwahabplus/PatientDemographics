using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatientDemographics.Logger
{
    public static class LogHelper
    {
        private static LogBase _logger = null;
        
        //this class should be used to log message
        public static void Log(string message)
        {
            _logger = new FileLogger();
            _logger.Log(message);
        }
    }
}
