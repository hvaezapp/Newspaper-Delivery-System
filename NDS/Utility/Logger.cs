using NDS.Models.Services;
using Microsoft.AspNetCore.Hosting;
using System;
using System.IO;
using System.Threading.Tasks;

namespace NDS.Utility
{
    public class Logger : ILogger
    {

        private  IWebHostEnvironment _appEnvironment;

        public Logger(IWebHostEnvironment appEnvironment)
        {
            _appEnvironment = appEnvironment;
        }


        public void Log(string message, string source)
        {
            try
            {

                string path = Path.Combine(_appEnvironment.WebRootPath, "log/log.txt");

                string content = "Time :: " + DateTime.Now.ToString() + " => " + " Message :: " + message +

                                  "=> Source :: " + source + " \r\n ================================================================= \r\n";


                 File.AppendAllText(path, content);

                
            }
            catch
            {

            }
        }
    }
}
