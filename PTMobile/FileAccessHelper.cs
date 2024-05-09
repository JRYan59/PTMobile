using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace PTMobile
{
    public class FileAccessHelper
    {
        public static string GetLocalFilePath(string filename)
        {
#if WINDOWS
            
            string folderPath = System.IO.Path.Combine(Directory.GetCurrentDirectory(),"Data");
            System.IO.Directory.CreateDirectory(folderPath);

            
            return System.IO.Path.Combine(folderPath,filename);
#endif
            return System.IO.Path.Combine(FileSystem.AppDataDirectory, filename);
        }
    }
}
