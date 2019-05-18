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
        /// <summary>
        /// 创建一个 AuthenticationCodeBuilder 实例
        /// </summary>
        /// <returns>新的 AuthenticationCodeBuilder 实例</returns>
        private AuthenticationCodeBuilder MakeAuthenticationCodeBuilder()
            => new AuthenticationCodeBuilder();
        /// <summary>
        /// 如果 CodeLength 为 4, 则生成的验证码的长度应该为 4
        /// </summary>
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
