using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using Discovery.Core.Interfaces;

namespace Discovery.Service
{
    /// <summary>
    /// 用于与服务器上的 FTP 服务交互
    /// </summary>
    public class WebFileService : IWebFileService
    {
        /// <summary>
        /// FTP 服务器完整路径
        /// </summary>
        private readonly Uri _ftpServiceAddress;

        /// <summary>
        /// 登录凭据
        /// </summary>
        private readonly NetworkCredential _credentials;

        /// <summary>
        /// 实例化一个FtpService 对象
        /// </summary>
        /// <param name="ftpServiceAddress">FTP 服务器完整路径</param>
        /// <param name="userName">登录用户名(匿名登录留空)</param>
        /// <param name="password">登录密码(匿名登录留空)</param>
        public WebFileService(
            string ftpServiceAddress,
            string userName = "",
            string password = "")
            : this(new Uri(CorrectionFtpServiceAddress(ftpServiceAddress)), 
                   new NetworkCredential(userName, password))
        {
        }

        /// <summary>
        /// 实例化一个FtpService对象
        /// </summary>
        /// <param name="ftpServiceAddress"></param>
        /// <param name="credential"></param>
        public WebFileService(
            Uri ftpServiceAddress,
            NetworkCredential credential)
            => (_ftpServiceAddress, _credentials) = (ftpServiceAddress, credential);

        /// <summary>
        /// 校正 Ftp 服务器路径
        /// </summary>
        /// <param name="ftpServiceAddress">FTP 服务器路径</param>
        /// <returns>一个友好的Ftp服务器路径</returns>
        private static string CorrectionFtpServiceAddress(string ftpServiceAddress)
        {
            ftpServiceAddress = ftpServiceAddress.Replace(@"\", @"/");

            ftpServiceAddress = ftpServiceAddress.StartsWith("ftp://") ||
                                ftpServiceAddress.StartsWith("FTP://")
                                ? ftpServiceAddress
                                : "ftp://" + ftpServiceAddress;

            ftpServiceAddress = ftpServiceAddress.EndsWith(@"/")
                                ? ftpServiceAddress
                                : ftpServiceAddress + @"/";
            return ftpServiceAddress;
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
                    WebRequest.Create(
                        new Uri(_ftpServiceAddress, originFilePath)) as FtpWebRequest;
                newFtpWebRequest.Credentials = _credentials;
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
            int readedLength;
            do
            {
                readedLength = sourceStream.Read(buffer, 0, buffer.Length);
                targetStream.Write(buffer, 0, readedLength);
            } while (readedLength == buffer.Length);
        }

        /// <summary>
        /// 从 FTP 服务器下载文件到本地
        /// </summary>
        /// <param name="originFilePath">FTP服务器相对路径</param>
        /// <param name="localFilePath">本地绝对路径</param>
        /// <returns>True: 下载成功, 否则返回False</returns>
        public bool Download(string originFilepath, string localFilePath)
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
                    WebRequest.Create(
                        new Uri(_ftpServiceAddress, originFilePath)) as FtpWebRequest;
                newFtpWebRequest.Credentials = _credentials;
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
                    WebRequest.Create(
                        new Uri(_ftpServiceAddress, originFilePath)) as FtpWebRequest;
                newFtpWebRequest.Credentials = _credentials;
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
                    WebRequest.Create(
                        new Uri(_ftpServiceAddress, originDirectoryPath)) as FtpWebRequest;
                newFtpWebRequest.Credentials = _credentials;
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
        public bool FileIsExists(string originFilePath)
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

        /// <summary>
        /// 在FTP服务器指定路径创建一个新的目录
        /// </summary>
        /// <param name="originDirectoryPath">目录相对路径(包括目录名)</param>
        /// <returns>True: 创建成功。 否则返回 False</returns>
        public bool MakeDirectory(string originDirectoryPath)
        {
            try
            {
                return MakeDirectoryCore(originDirectoryPath);
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 在FTP服务器指定路径创建一个新的目录
        /// </summary>
        /// <param name="originDirectoryPath">目录相对路径(包括目录名)</param>
        /// <returns>True: 创建成功。 否则抛出异常</returns>
        private bool MakeDirectoryCore(string originDirectoryPath)
        {
            FtpWebRequest ftpWebRequest = MakeNewFtpWebRequest();
            using (var response = ftpWebRequest.GetResponse() as FtpWebResponse)
            using (Stream originFileStream = response.GetResponseStream())
            {
                return true;
            }
            FtpWebRequest MakeNewFtpWebRequest()
            {
                var newFtpWebRequest = 
                    WebRequest.Create(
                        new Uri(_ftpServiceAddress, originDirectoryPath)) as FtpWebRequest;
                newFtpWebRequest.Credentials = _credentials;
                newFtpWebRequest.Method = WebRequestMethods.Ftp.MakeDirectory;
                newFtpWebRequest.UseBinary = true;
                newFtpWebRequest.UsePassive = false;
                return newFtpWebRequest;
            }
        }
    }
}
