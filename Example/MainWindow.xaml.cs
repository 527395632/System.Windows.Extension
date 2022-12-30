using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Extension.Controls;
using System.Windows.Extension.Data;
using System.Windows.Extension.Interactivity;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Example
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : System.Windows.Window
    {
        public MainWindow()
        {
            InitializeComponent();
            ClickCommand = new Command<UserInfo>(OnClick, OnClickValidate);
            StateChangeCommand = new Command<TreeViewItem>(OnStateChange);
            DataContext = this;
        }

        public Command<UserInfo> ClickCommand { get; }
        private void OnClick(UserInfo info)
        {
        }
        private bool OnClickValidate(UserInfo info)
        {
            return true;
        }


        public Command<TreeViewItem> StateChangeCommand { get; }
        private void OnStateChange(TreeViewItem item)
        {
        }
    }
}