using FluentFTP;
using Microsoft.Maui.Storage;
using PTMobile.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTMobile
{
    public class FTPService
    {
        Log log = new Log();

        static string _user;
        static string _password;
        static string _ftpUri;
        static int _port;
        enum FileStatus { NotExits,MinorVer,MayorVer,Equal};

        public FTPService(string user, string password, string ftpUri, int port)
        {
            _user = user;
            _password = password;
            _ftpUri = ftpUri;
            _port = port;

        }

        public async Task DownloadFileAsync(string fileName, string path)
        {
            try
            {
                var token = new CancellationToken();
                using (var ftp = new AsyncFtpClient(_ftpUri, _user, _password, _port))
                {
                    await ftp.Connect(token);
                    // download a file and ensure the local directory is created
                    if (ftp.FileExists(fileName).Result)
                        await ftp.DownloadFile(path, fileName, FtpLocalExists.Overwrite, FtpVerify.Retry);

                    //// download a file and ensure the local directory is created, verify the file after download
                    //ftp.DownloadFile(@"D:\Github\FluentFTP\README.md", "/public_html/temp/README.md", FtpLocalExists.Overwrite, FtpVerify.Retry);
                }
            }
            catch (Exception e)
            {

                log.WriteInLog("FTPService.DownloadFileAsync. ", e.Message);
            }
            
        }
        public async Task UploadLocalFile(string fileName,string path)
        {
            try
            {
                var token = new CancellationToken();
                using (var ftp = new AsyncFtpClient(_ftpUri, _user, _password, _port))
                {
                    await ftp.Connect(token);

                    // upload a file to an existing FTP directory

                    //ftp.UploadFile(_localFolder + fileName, fileName, FtpRemoteExists.Overwrite, true, FtpVerify.Retry);
                    if (File.Exists(path))
                        await ftp.UploadFile(path, fileName, FtpRemoteExists.Overwrite, true, FtpVerify.Retry);

                }
            }
            catch (Exception e)
            {
                log.WriteInLog("FTPService.UploadLocalFile. ", e.Message);

            }

        }

        public List<string> GetItems()
        {
            try
            {
                List<string> items = new();
                using (var conn = new FtpClient(_ftpUri, _user, _password, _port))
                {
                    foreach (var item in conn.GetListing("", FtpListOption.NameList))
                    {
                        items.Add(item.Name);
                    }
                }
                return items;
            }
            catch (Exception e)
            {

                log.WriteInLog("FTPService.GetItems. ", e.Message);
                return new List<string>();
            }
            
        }

        FileStatus FileExists(string fileName,bool compareDate,DateTime dateLocalFile)
        {
            try
            {
                FileStatus fileStatus = FileStatus.NotExits;
                using (var conn = new FtpClient(_ftpUri, _user, _password, _port))
                {
                    bool fileExists = conn.FileExists(fileName);
                    if (fileExists)
                    {
                        if (compareDate)
                        {
                            //conn.GetModifiedTime("");
                            //si la fecha del archivo REMOTO es menor que el archivo local (dateLocalFile) devuelve false
                            string comparacionfechas = "";
                            switch (comparacionfechas)
                            {
                                case "fechamayorarriba":
                                    fileStatus = FileStatus.MayorVer;
                                    break;
                                case "fechamenorarriba":
                                    fileStatus = FileStatus.MinorVer;
                                    break;
                                default:
                                    fileStatus = FileStatus.Equal;
                                    break;
                            }
                        }
                        else
                            fileStatus = FileStatus.Equal;
                    }
                }
                return fileStatus;
            }
            catch (Exception e)
            {
                log.WriteInLog("FTPService.FileExists. ", e.Message);
                return FileStatus.NotExits;
            }

        }

        public async void Sync()
        {
            try
            {
                List<ProductImage> Images = App.CashierData.GetAllProductImages();
                foreach (var item in Images)
                {
                    if (!File.Exists(item.Path))
                    {
                        if (FileExists(item.Name, false, DateTime.MinValue) != FileStatus.NotExits)
                            await this.DownloadFileAsync(item.Name, item.Path);
                    }
                    else
                    {
                        FileInfo fileInfo = new FileInfo(item.Path);
                        DateTime date = fileInfo.LastWriteTime;
                        var resultFile = FileExists(item.Name, true, date);
                        switch (resultFile)
                        {
                            case FileStatus.NotExits:
                                break;
                            case FileStatus.MinorVer:
                                await this.UploadLocalFile(item.Name, item.Path);
                                break;
                            case FileStatus.MayorVer:
                                await this.DownloadFileAsync(item.Name, item.Path);
                                break;
                            case FileStatus.Equal:
                                break;
                            default:
                                break;
                        }
                    }
                }
                if (Preferences.ContainsKey("LastSync"))
                {
                    Preferences.Remove("LastSync");
                    Preferences.Set("LastSync",DateTime.Now);
                }
            }
            catch (Exception e)
            {
                log.WriteInLog("FTPService.Sync. ", e.Message);


            }
            //Thread.Sleep(100000);
        }
    }
}