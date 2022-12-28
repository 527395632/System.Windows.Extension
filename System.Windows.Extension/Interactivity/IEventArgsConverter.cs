namespace System.Windows.Extension.Interactivity
{
    public interface IEventArgsConverter
    {
        object Convert(object value, object parameter);
    }
}