using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Extension.Langs;

namespace Example
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            var t = new UserInfo().GetType();

            base.OnStartup(e);
            Lang.CurrentLanguage = "zh-CN";
        }
    }
}
