using System.Diagnostics.CodeAnalysis;
using System.Security;
using System.Security.Permissions;

namespace System.Windows.Extension.Tools
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    internal class SecurityHelper
    {
        private static UIPermission _allWindowsUIPermission;

        [SecurityCritical]
        internal static void DemandUIWindowPermission()
        {
            _allWindowsUIPermission ??= new UIPermission(UIPermissionWindow.AllWindows);
            _allWindowsUIPermission.Demand();
        }
    }
}