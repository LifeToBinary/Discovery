using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Discovery.Client.Resource.Themes
{
    public partial class PinkTheme : ResourceDictionary
    {
        private PinkTheme() => InitializeComponent();
        public static PinkTheme Instance { get; } = new PinkTheme();
    }
}
