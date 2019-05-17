using Discovery.Core.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Discovery.Core.GlobalData
{
    public class GlobalObjectHolder
    {
        public static Discoverer CurrentUser { get; set; }
    }
}
