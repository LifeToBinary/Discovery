using System;
using System.Collections.Generic;
using System.Text;

namespace Discovery.Core.Tools
{
    /// <summary>
    /// 验证码生成器
    /// </summary>
    public class AuthenticationCodeBuilder
    {
        /// <summary>
        /// 验证码长度
        /// </summary>
        public int CodeLength { get; set; }

        /// <summary>
        /// 验证码字符范围
        /// </summary>
        public string CharsSource { get; set; }

        /// <summary>
        /// 随机器
        /// </summary>
        private readonly Random _random = 
            new Random(DateTime.Now.Ticks.GetHashCode());

        /// <summary>
        /// 生成验证码
        /// </summary>
        /// <returns>验证码</returns>
        public string Build()
        {
            var result = new StringBuilder();
            for (int i = 0; i < CodeLength; ++i)
            {
                char randomChar = CharsSource[_random.Next(CharsSource.Length)];
                result.Append(randomChar);
            }
            return $"{result}";
        }
    }
}
