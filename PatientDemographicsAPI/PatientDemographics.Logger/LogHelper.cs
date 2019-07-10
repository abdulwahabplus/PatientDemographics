using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatientDemographics.Logger
{
    public static class LogHelper
    {
        private static LogBase logger = null;
        public static void Log(string message)
        {
            logger = new FileLogger();
            logger.Log(message);
        }
    }
}
