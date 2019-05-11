using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Discovery.Service.Test
{
    [TestFixture]
    public class FTPServiceTests
    {
        private FTPService MakeNewFTPService()
            => new FTPService(string ftpAddress, string ftpRemotePath);
    }
}
