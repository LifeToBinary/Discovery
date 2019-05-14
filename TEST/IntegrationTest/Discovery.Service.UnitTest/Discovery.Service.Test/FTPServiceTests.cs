using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Discovery.Service.Test
{
    [TestFixture]
    public class FtpServiceTests
    {
        private WebFileService MakeNewFTPService()
            => new WebFileService(@"ftp://47.240.12.27");
    }
}
