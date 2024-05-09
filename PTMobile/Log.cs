using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTMobile
{
    public class Log
    {
#if WINDOWS
				string folder = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
#else
        string folder = FileSystem.AppDataDirectory;
#endif
        public bool WriteInLog(string command, string response)
        {
            string directory =  folder+ "\\log";
            string route = directory + "\\log_" + DateTime.Now.ToString("dd-MM-yyyy-") + ".txt";

            if (Directory.Exists(directory) == false)
            {
                Directory.CreateDirectory(directory);
            }
            else
            {
                if (System.IO.File.Exists(route) == false)
                {
                    System.IO.File.AppendAllText(route, DateTime.Now.ToString("hh:mm:ss tt") + " \t" + command + " \t" + response);
                }
                else
                {
                    System.IO.File.AppendAllText(route, "\r\n" + DateTime.Now.ToString("hh:mm:ss tt") + " \t" + command + " \t" + response);
                }
            }

            return File.Exists(route);
        }

        public string OpenLog(DateTime date)
        {
            string directory = folder + "\\log";
            string route = directory + "\\log_" + date.ToString("dd-MM-yyyy-") + ".txt";

            if (File.Exists(route))
            {
                return File.OpenText(route).ReadToEnd();

            }
            else
            {
                return "NE";
            }
        }
    }
}
