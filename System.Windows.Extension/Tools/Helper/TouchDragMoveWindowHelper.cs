using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Extension.Tools.Interop;

namespace System.Windows.Extension.Tools.Helper
{
    internal class TouchDragMoveWindowHelper
    {
        private const int MaxMoveSpeed = 60;

        public TouchDragMoveWindowHelper(Window window)
        {
            _window = window;
        }

        public void Start()
        {
            var window = _window;

            window.PreviewMouseMove += Window_PreviewMouseMove;
            window.PreviewMouseUp += Window_PreviewMouseUp;
            window.LostMouseCapture += Window_LostMouseCapture;
        }

        public void Stop()
        {
            var window = _window;

            window.PreviewMouseMove -= Window_PreviewMouseMove;
            window.PreviewMouseUp -= Window_PreviewMouseUp;
            window.LostMouseCapture -= Window_LostMouseCapture;
        }

        private readonly Window _window;

        private InteropValues.POINT? _lastPoint;

        private void Window_LostMouseCapture(object sender, MouseEventArgs e) => Stop();

        private void Window_PreviewMouseUp(object sender, MouseButtonEventArgs e) => Stop();

        private void Window_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            InteropMethods.GetCursorPos(out var lpPoint);

            if (_lastPoint == null)
            {
                _lastPoint = lpPoint;
                _window.CaptureMouse();
            }

            var dx = lpPoint.X - _lastPoint.Value.X;
            var dy = lpPoint.Y - _lastPoint.Value.Y;

            if (Math.Abs(dx) < MaxMoveSpeed && Math.Abs(dy) < MaxMoveSpeed)
            {
                var handle = new WindowInteropHelper(_window).Handle;

                InteropMethods.GetWindowRect(handle, out var lpRect);

                InteropMethods.SetWindowPos(handle, IntPtr.Zero, lpRect.Left + dx, lpRect.Top + dy, 0, 0,
                    (int)(InteropValues.WindowPositionFlags.SWP_NOSIZE |
                           InteropValues.WindowPositionFlags.SWP_NOZORDER));
            }

            _lastPoint = lpPoint;
        }
    }
}