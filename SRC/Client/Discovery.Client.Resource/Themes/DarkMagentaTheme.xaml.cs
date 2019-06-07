﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Discovery.Client.Resource.Themes
{
    public partial class DarkMagentaTheme : ResourceDictionary
    {
        private DarkMagentaTheme() => InitializeComponent();
        public static DarkMagentaTheme Instance { get; } = new DarkMagentaTheme();
    }
}
