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
using System.Windows.Extension;
using System.Windows.Extension.Controls;
using System.Windows.Extension.Data;
using System.Windows.Extension.Interactivity;
using System.Windows.Extension.Mvvm;
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
            DataContext = new MainWindowViewModel();
        }
    }

    public class MainWindowViewModel : ViewModelBase
    {
        [CommandBinding(nameof(OnClick), nameof(OnCanClick))]
        public Command ClickCommand { get; private set; }

        private void OnClick(string args1, TextBlock control, string args2)
        {
        }

        private bool OnCanClick(string args1, TextBlock control, string args2)
        {
            return true;
        }


        [CommandBinding(nameof(OnStateChange))]
        public Command StateChangeCommand { get; private set; }
        private void OnStateChange(string args1, TextBlock control, string args2)
        {
        }
    }
}