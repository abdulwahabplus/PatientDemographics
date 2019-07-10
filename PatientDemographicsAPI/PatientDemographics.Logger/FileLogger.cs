using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatientDemographics.Logger
{
    public class FileLogger : LogBase
    {
        public string filePath = System.Configuration.ConfigurationSettings.AppSettings["LogFilePath"].ToString();
        public override void Log(string message)
        {
            lock (lockObj)
            {
                if (!string.IsNullOrWhiteSpace(filePath))
                {
                    using (StreamWriter streamWriter = new StreamWriter(filePath))
                    {
                        streamWriter.WriteLine(message);
                        streamWriter.Close();
                    } 
                }
            }
        }
    }
}
