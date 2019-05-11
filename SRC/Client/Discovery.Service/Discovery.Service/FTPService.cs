using System;
using System.Collections.Generic;
using System.Text;

namespace Discovery.Service
{
    public class FTPService
    {
        private readonly string _ftpServiceAddress;
        public FTPService(string ftpServiceAddress)
            => _ftpServiceAddress = ftpServiceAddress;
    }
}
