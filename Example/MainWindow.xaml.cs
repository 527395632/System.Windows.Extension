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
            ClickCommand = new Command();
            ClickCommand.OnCommandExecute += OnClick;
            ClickCommand.OnCommandCanExecute += OnClickValidate;

            StateChangeCommand = new Command();
            StateChangeCommand.OnCommandExecute += OnStateChange;

            DataContext = this;
        }

        public Command ClickCommand { get; }


        private void OnClick(object info)
        {
        }

        private bool OnClickValidate(object info)
        {
            return true;
        }


        public Command StateChangeCommand { get; }
        private void OnStateChange(object item)
        {
        }
    }
}