using Discovery.Core.Tools;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Discovery.Core.UnitTest
{
    [TestFixture]
    public class AuthenticationCodeBuilderTests
    {
        private AuthenticationCodeBuilder MakeAuthenticationCodeBuilder()
            => new AuthenticationCodeBuilder();
        [Test]
        public void Build_CountIs4_ResultCountIs4()
        {
            AuthenticationCodeBuilder builder = MakeAuthenticationCodeBuilder();
            builder.CodeLength = 4;
            builder.CharsSource = "ABCDEFG12345";
            int expected = 4;

            int actual = builder.Build().Length;
            Assert.AreEqual(expected, actual);
        }
    }
}
