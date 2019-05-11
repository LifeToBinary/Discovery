using System;
using System.Collections.Generic;
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
        /// 实例化一个FTPService 对象
        /// </summary>
        /// <param name="ftpServiceAddress"> FTP 服务器完整路径</param>
        public FTPService(string ftpServiceAddress)
            => _ftpServiceAddress = ftpServiceAddress;
    }
}
