using System;
using System.Collections.Generic;
using System.Text;

namespace Discovery.Core.Tools
{
    public class AuthenticationCodeBuilder
    {
        public int CodeLength { get; set; }
        public string CharsSource { get; set; }
        private readonly Random _random = 
            new Random(DateTime.Now.Ticks.GetHashCode());
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
