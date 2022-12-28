﻿using System.Windows.Extension.Langs;
using System.Windows.Media;

namespace System.Windows.Extension.Data
{
    public class GrowlInfo
    {
        public string Message { get; set; }

        public bool ShowDateTime { get; set; } = true;

        public int WaitTime { get; set; } = 6;

        public string CancelStr { get; set; } = Lang.CurrentLanguage.Lang_Cancel;

        public string ConfirmStr { get; set; } = Lang.CurrentLanguage.Lang_Confirm;

        public Func<bool, bool> ActionBeforeClose { get; set; }

        public bool StaysOpen { get; set; }

        public bool IsCustom { get; set; }

        public InfoType Type { get; set; }

        public Geometry Icon { get; set; }

        public string IconKey { get; set; }

        public Brush IconBrush { get; set; }

        public string IconBrushKey { get; set; }

        public bool ShowCloseButton { get; set; } = true;

        public string Token { get; set; }

        public FlowDirection FlowDirection { get; set; }
    }
}