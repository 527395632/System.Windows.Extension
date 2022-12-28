﻿using System.Runtime.InteropServices;
using System.Windows.Extension.Data;
using System.Windows.Extension.Tools.Interop;
using Microsoft.Win32;

namespace System.Windows.Extension.Tools.Helper
{
    internal class SystemHelper
    {
        public static SystemVersionInfo GetSystemVersionInfo()
        {
            var osv = new InteropValues.RTL_OSVERSIONINFOEX();
            osv.dwOSVersionInfoSize = (uint)Marshal.SizeOf(osv);
            InteropMethods.Gdip.RtlGetVersion(out osv);
            return new SystemVersionInfo((int)osv.dwMajorVersion, (int)osv.dwMinorVersion, (int)osv.dwBuildNumber);
        }

        private const string SkinTypeRegistryKeyName = @"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Themes\Personalize";

        private const string SkinTypeRegistryValueName = "AppsUseLightTheme";

        public static bool DetermineIfInLightThemeMode()
        {
            var value = Registry.GetValue(SkinTypeRegistryKeyName, SkinTypeRegistryValueName, "0");

            return value != null && value.ToString() != "0";
        }
    }
}