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
        //File path has to be configured in web.config
        public string filePath = System.Configuration.ConfigurationSettings.AppSettings["LogFilePath"].ToString();
        
        /// <summary>
        /// Log message to file
        /// </summary>
        /// <param name="message"></param>
        public override void Log(string message)
        {
            try
            {
                lock (lockObj)
                {
                    if (!string.IsNullOrWhiteSpace(filePath))
                    {
                        string fullPath = System.AppDomain.CurrentDomain.BaseDirectory + filePath;
                        if (File.Exists(fullPath))
                        {
                            using (StreamWriter streamWriter = new StreamWriter(fullPath))
                            {
                                streamWriter.WriteLine(message);
                                streamWriter.Close();
                            } 
                        }
                    }
                }
            }
            catch
            {

            }
        }
    }
}
