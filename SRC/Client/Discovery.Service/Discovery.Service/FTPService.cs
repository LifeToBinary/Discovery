using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace Discovery.Service
{
    public class FtpService
    {
        /// <summary>
        /// FTP 服务器完整路径
        /// </summary>
        private readonly string _ftpServiceAddress;

        /// <summary>
        /// FTP 服务器登录用户名
        /// </summary>
        private readonly string _userName;

        /// <summary>
        /// FTP 服务器登录密码
        /// </summary>
        private readonly string _password;

        /// <summary>
        /// 实例化一个FTPService 对象
        /// </summary>
        /// <param name="ftpServiceAddress"> FTP 服务器完整路径</param>
        public FtpService(string ftpServiceAddress)
            => new FtpService(ftpServiceAddress, String.Empty, String.Empty);

        /// <summary>
        /// 实例化一个FTPService 对象
        /// </summary>
        /// <param name="ftpServiceAddress">FTP 服务器完整路径</param>
        /// <param name="userName">登录用户名(匿名登录留空)</param>
        /// <param name="password">登录密码(匿名登录留空)</param>
        public FtpService(
            string ftpServiceAddress,
            string userName,
            string password)
        {
            _ftpServiceAddress = ftpServiceAddress;
            _userName = userName;
            _password = password;
        }

        /// <summary>
        /// 上传文件到 FTP 服务器
        /// </summary>
        /// <param name="localFilePath">本地文件完整路径</param>
        /// <param name="originFilePath">FTP服务器相对路径</param>
        /// <returns> True: 上传成功, False: 上传失败, 上传过程中, 发生了错误</returns>
        public bool Upload(string localFilePath, string originFilePath)
        {
            try
            {
                return UploadCore(localFilePath, originFilePath);
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 上传文件到 FTP 服务器
        /// </summary>
        /// <param name="localFilePath">本地文件绝对路径</param>
        /// <param name="originFilePath">FTP服务器相对路径</param>
        /// <returns>如果在上传过程中没有发生错误, 则返回 True, 否则抛出异常</returns>
        private bool UploadCore(string localFilePath, string originFilePath)
        {
            FtpWebRequest ftpWebRequest = MakeNewFTPWebRequest();
            using (FileStream localFileStream = File.OpenRead(localFilePath))
            using (Stream originFileStream = ftpWebRequest.GetRequestStream())
            {
                CopyData(localFileStream, originFileStream, new byte[2048]);
                return true;
            }

            // 创建一个 FtpWebRequest 对象用于上传文件
            FtpWebRequest MakeNewFTPWebRequest()
            {
                var newFtpWebRequest =
                    WebRequest.Create(new Uri(
                        Path.Combine(_ftpServiceAddress, originFilePath))) as FtpWebRequest;
                newFtpWebRequest.Credentials = new NetworkCredential(_userName, _password);
                newFtpWebRequest.KeepAlive = false;
                newFtpWebRequest.Method = WebRequestMethods.Ftp.UploadFile;
                newFtpWebRequest.UseBinary = true;
                newFtpWebRequest.ContentLength = new FileInfo(localFilePath).Length;
                newFtpWebRequest.UsePassive = false;
                return newFtpWebRequest;
            }
        }

        /// <summary>
        /// 把一个流的数据读取到另一个流中
        /// </summary>
        /// <param name="sourceStream">源</param>
        /// <param name="targetStream">目标</param>
        /// <param name="buffer">中间缓存</param>
        private void CopyData(Stream sourceStream,
                              Stream targetStream,
                              byte[] buffer)
        {
            int readLength = sourceStream.Read(buffer, 0, buffer.Length);
            targetStream.Write(buffer, 0, readLength);
            if (readLength < buffer.Length)
            {
                return;
            }
            CopyData(sourceStream, targetStream, buffer);
        }

        /// <summary>
        /// 从 FTP 服务器下载文件到本地
        /// </summary>
        /// <param name="originFilePath">FTP服务器相对路径</param>
        /// <param name="localFilePath">本地绝对路径</param>
        /// <returns>True: 下载成功, 否则返回False</returns>
        public bool DownLoad(string originFilepath, string localFilePath)
        {
            try
            {
                return DownloadCore(originFilepath, localFilePath);
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 从 FTP 服务器下载文件到本地
        /// </summary>
        /// <param name="originFilePath">FTP服务器相对路径</param>
        /// <param name="localFilePath">本地绝对路径</param>
        /// <returns>True: 下载成功, 否则抛出异常</returns>
        private bool DownloadCore(string originFilePath, string localFilePath)
        {
            FtpWebRequest ftpWebRequest = MakeNewFTPWebRequest();
            using (var response = ftpWebRequest.GetResponse() as FtpWebResponse)
            using (var localFileStream = new FileStream(localFilePath, FileMode.Create))
            using (Stream originFileStream = response.GetResponseStream())
            {
                CopyData(originFileStream, localFileStream, new byte[2048]);
                return true;
            }

            // 创建一个FtpWebRequest 实例, 用于从 FTP 服务器下载文件
            FtpWebRequest MakeNewFTPWebRequest()
            {
                var newFtpWebRequest =
                    WebRequest.Create(new Uri(
                        Path.Combine(_ftpServiceAddress, originFilePath))) as FtpWebRequest;
                newFtpWebRequest.Credentials = new NetworkCredential(_userName, _password);
                newFtpWebRequest.Method = WebRequestMethods.Ftp.DownloadFile;
                newFtpWebRequest.UseBinary = true;
                newFtpWebRequest.UsePassive = false;
                return newFtpWebRequest;
            }
        }

        /// <summary>
        /// 删除 FTP 服务器上的指定文件
        /// </summary>
        /// <param name="originFilePath">文件的相对路径(包括文件名)</param>
        /// <returns>True: 删除成功, 否则返回False</returns>
        public bool Delete(string originFilePath)
        {
            try
            {
                return DeleteCore(originFilePath);
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 删除 FTP 服务器上的指定文件
        /// </summary>
        /// <param name="originFilePath">文件的相对路径(包括文件名)</param>
        /// <returns>True: 删除成功, 否则抛出异常</returns>
        private bool DeleteCore(string originFilePath)
        {
            FtpWebRequest ftpWebRequest = MakeNewFtpWebRequest();
            using (var response = ftpWebRequest.GetResponse() as FtpWebResponse)
            using (Stream originFileStream = response.GetResponseStream())
            using (var originFileReader = new StreamReader(originFileStream))
            {
                originFileReader.ReadToEnd();
                return true;
            }

            // 创建一个 FtpWebRequest 实例, 用于删除 FTP 服务器上的文件
            FtpWebRequest MakeNewFtpWebRequest()
            {
                var newFtpWebRequest =
                    WebRequest.Create(new Uri(
                        Path.Combine(_ftpServiceAddress, originFilePath))) as FtpWebRequest;
                newFtpWebRequest.Credentials = new NetworkCredential(_userName, _password);
                newFtpWebRequest.KeepAlive = false;
                newFtpWebRequest.Method = WebRequestMethods.Ftp.DeleteFile;
                newFtpWebRequest.UsePassive = false;
                return newFtpWebRequest;

            }
        }

        /// <summary>
        /// 获取服务器指定目录下的文件名列表
        /// </summary>
        /// <param name="originDirectoryPath">目录相对路径</param>
        /// <returns>此目录下的文件名列表</returns>
        public List<string> GetAllFilesList(string originDirectoryPath)
        {
            FtpWebRequest ftpWebRequest = MakeNewFtpWebRequest();
            using (WebResponse response = ftpWebRequest.GetResponse())
            using (var reader = new StreamReader(response.GetResponseStream()))
            {
                return GetAllFileListFromStream(reader)
                           .Select(file => file.Split(' ').Last())
                           .Where(fileName => !String.IsNullOrEmpty(fileName) && 
                                              !String.IsNullOrEmpty(Path.GetExtension(fileName)))
                           .ToList();
            }

            // 获取一个流中的所有文件
            IEnumerable<string> GetAllFileListFromStream(StreamReader reader)
            {
                string fileName = String.Empty;
                while (true)
                {
                    fileName = reader.ReadLine();
                    if (fileName is null)
                    {
                        break;
                    }
                    yield return fileName;
                };
            }

            // 创建 FtpWebRequest 对象, 用于获取服务器指定目录下的所有文件
            FtpWebRequest MakeNewFtpWebRequest()
            {
                var newFtpWebRequest =
                    WebRequest.Create(new Uri(
                        Path.Combine(_ftpServiceAddress, originDirectoryPath))) as FtpWebRequest;
                newFtpWebRequest.Credentials = new NetworkCredential(_userName, _password);
                newFtpWebRequest.Method = WebRequestMethods.Ftp.ListDirectory;
                newFtpWebRequest.UsePassive = false;
                return newFtpWebRequest;
            }
        }

        /// <summary>
        /// 检查一个文件是否存在与 FTP 服务器上
        /// </summary>
        /// <param name="originFilePath">文件的相对路径</param>
        /// <returns>True: 存在, False: 不存在</returns>
        public bool IsExistsOnTheFtpServer(string originFilePath)
        {
            string directoryPath = Path.GetDirectoryName(originFilePath);
            string fileFullName = Path.GetFileName(originFilePath);
            List<string> filesListInTheDirectory = GetAllFilesList(directoryPath);
            return filesListInTheDirectory.Count(file => file == fileFullName) >= 1;
        }

        /// <summary>
        /// 更新 FTP 服务器上的文件
        /// </summary>
        /// <param name="localFilePath">本地文件完整路径</param>
        /// <param name="originFilePath">FTP 服务器相对路径</param>
        /// <returns>True: 更新成功  False: 更新失败, 更新过程中, 发生了错误</returns>
        public bool UpdateFile(string localFilePath, string originFilePath)
            => Upload(localFilePath, originFilePath);
    }
}
