using System.Windows.Controls;
using System.Windows.Extension.Mvvm;

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
        [CmdBinding(nameof(OnClick), nameof(OnCanClick))]
        public Command ClickCommand { get; private set; }

        private void OnClick(string args1, TextBlock control, string args2)
        {
        }

        private bool OnCanClick(string args1, TextBlock control, string args2)
        {
            return true;
        }


        [CmdBinding(nameof(OnStateChange))]
        public Command StateChangeCommand { get; private set; }
        private void OnStateChange(string args1, TreeViewItem control, string args2)
        {
        }
    }
}