﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace Discovery.Service
{
    public class FTPService
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
        public FTPService(string ftpServiceAddress)
            => new FTPService(ftpServiceAddress, String.Empty, String.Empty);

        /// <summary>
        /// 实例化一个FTPService 对象
        /// </summary>
        /// <param name="ftpServiceAddress">FTP 服务器完整路径</param>
        /// <param name="userName">登录用户名(匿名登录留空)</param>
        /// <param name="password">登录密码(匿名登录留空)</param>
        public FTPService(
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
        /// <param name="isSuccess">如果上传成功 为True, 否则为 False</param>
        /// <returns> 当前 FTPService 实例</returns>
        public FTPService Upload(string localFilePath, 
                                 string originFilePath, 
                                 out bool isSuccess)
        {
            try
            {
                return UploadCore(localFilePath, originFilePath, out isSuccess);
            }
            catch
            {
                isSuccess = false;
                return this;
            }
        }

        /// <summary>
        /// 上传文件到 FTP 服务器
        /// </summary>
        /// <param name="localFilePath">本地文件完整路径</param>
        /// <param name="isSuccess">如果上传成功 为True </param>
        /// <param name="originFilePath">FTP服务器相对路径</param>
        /// <returns>如果在上传过程中没有发生错误, 则返回 True, 否则抛出异常</returns>
        private FTPService UploadCore(string localFilePath, 
                                      string originFilePath, 
                                      out bool isSuccess)
        {
            FtpWebRequest ftpWebRequest = MakeNewFTPWebRequest();
            using (FileStream localFileStream = File.OpenRead(localFilePath))
            using (Stream originFileStream = ftpWebRequest.GetRequestStream())
            {
                CopyData(localFileStream, originFileStream, new byte[2048]);
                isSuccess = true;
                return this;
            }

            // 创建一个 FtpWebRequest 对象用于上传文件
            FtpWebRequest MakeNewFTPWebRequest()
            {
                var newFtpWebRequest =
                    WebRequest.Create(
                        new Uri(Path.Combine(_ftpServiceAddress, originFilePath))) as FtpWebRequest;
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
    }
}
