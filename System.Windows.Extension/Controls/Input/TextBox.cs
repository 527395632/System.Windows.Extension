using System.Windows.Input;
using System.Windows.Extension.Interactivity;

namespace System.Windows.Extension.Controls
{
    public class TextBox : System.Windows.Controls.TextBox
    {
        public TextBox()
        {
            CommandBindings.Add(new CommandBinding(ControlCommands.Clear, (s, e) =>
            {
                SetCurrentValue(TextProperty, string.Empty);
            }));
        }
    }
}