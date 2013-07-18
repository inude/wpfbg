using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Windows;

namespace WpfApplication1
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            CultureInfo ci = new CultureInfo(Thread.CurrentThread.CurrentCulture.Name);
            ci.DateTimeFormat.ShortDatePattern = "yyyy-MM-dd";
            ci.DateTimeFormat.DateSeparator = "-";
            Thread.CurrentThread.CurrentCulture = ci;
        }
    }
}
