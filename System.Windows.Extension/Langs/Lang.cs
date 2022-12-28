using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Windows;
using System.Windows.Data;
using System.Windows.Extension.Tools;

namespace System.Windows.Extension.Langs
{
    public sealed class Lang : ResourceDictionary, INotifyPropertyChanged
    {
        private CultureInfo _culture;

        #region INotifyPropertyChanged
        private PropertyChangedEventHandler _notify;
        event PropertyChangedEventHandler INotifyPropertyChanged.PropertyChanged
        {
            add => _notify += value;
            remove => _notify -= value;
        }
        #endregion

        #region Propertys
        /// <summary>
        /// 语言信息
        /// </summary>
        public CultureInfo Culture
        {
            get => _culture;
            set
            {
                _culture = value;
                /*
                 * 加载语言
                 */
                UpdateLangs();
            }
        }

        public static Lang CurrentLanguage
        {
            get
            {
                if (Application.Current.Resources.MergedDictionaries.FirstOrDefault(q => q is Lang) is Lang lang)
                {
                    return lang;
                }
                return null;
            }
        }
        #endregion

        #region INotifyPropertyChanged
        private void OnPropertyChanged(SystemLangKeys key) => _notify?.Invoke(this, new PropertyChangedEventArgs(key.ToString()));
        private void UpdateLangs()
        {
            foreach (SystemLangKeys item in Enum.GetValues(typeof(SystemLangKeys)))
            {
                OnPropertyChanged(item);
            }
        }
        #endregion

        #region GetLanguage & SetLanguage
        public static string GetLanguage(SystemLangKeys key) => GetLanguage(key.ToString());

        /// <summary>
        /// 获取语言
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetLanguage(string key)
        {
            var lang = CurrentLanguage;
            if (lang == null)
            {
                lock (Application.Current.Resources)
                {
                    lang = CurrentLanguage;
                    if (lang == null)
                    {
                        lang = new Lang();
                        Application.Current.Resources.MergedDictionaries.Add(lang);
                    }
                }
            }
            return lang.Contains(key) ? lang[key]?.ToString() : string.Empty;
        }

        /// <summary>
        /// 设置语言
        /// </summary>
        /// <param name="dependencyObject"></param>
        /// <param name="dependencyProperty"></param>
        /// <param name="key"></param>
        public static void SetLanguage(DependencyObject dependencyObject, DependencyProperty dependencyProperty, SystemLangKeys key)
        {
            var lang = CurrentLanguage;
            if (lang == null)
            {
                lock (Application.Current.Resources)
                {
                    lang = CurrentLanguage;
                    if (lang == null)
                    {
                        lang = new Lang();
                        Application.Current.Resources.MergedDictionaries.Add(lang);
                    }
                }
            }
            BindingOperations.SetBinding(dependencyObject, dependencyProperty, new Binding(key.ToString())
            {
                Source = lang,
                Mode = BindingMode.OneWay
            });
        }
        #endregion

        #region 系统语言
        /// <summary>
        ///   查找类似 [全部] 的本地化字符串。
        /// </summary>
        public static string Lang_All => GetLanguage(SystemLangKeys.All);

        /// <summary>
        ///   查找类似 [上午] 的本地化字符串。
        /// </summary>
        public static string Lang_Am => GetLanguage(SystemLangKeys.Am);

        /// <summary>
        ///   查找类似 [取消] 的本地化字符串。
        /// </summary>
        public static string Lang_Cancel => GetLanguage(SystemLangKeys.Cancel);

        /// <summary>
        ///   查找类似 [清空] 的本地化字符串。
        /// </summary>
        public static string Lang_Clear => GetLanguage(SystemLangKeys.Clear);

        /// <summary>
        ///   查找类似 [关闭] 的本地化字符串。
        /// </summary>
        public static string Lang_Close => GetLanguage(SystemLangKeys.Close);

        /// <summary>
        ///   查找类似 [关闭所有] 的本地化字符串。
        /// </summary>
        public static string Lang_CloseAll => GetLanguage(SystemLangKeys.CloseAll);

        /// <summary>
        ///   查找类似 [关闭其他] 的本地化字符串。
        /// </summary>
        public static string Lang_CloseOther => GetLanguage(SystemLangKeys.CloseOther);

        /// <summary>
        ///   查找类似 [确定] 的本地化字符串。
        /// </summary>
        public static string Lang_Confirm => GetLanguage(SystemLangKeys.Confirm);

        /// <summary>
        ///   查找类似 [错误的图片路径] 的本地化字符串。
        /// </summary>
        public static string Lang_ErrorImgPath => GetLanguage(SystemLangKeys.ErrorImgPath);

        /// <summary>
        ///   查找类似 [非法的图片尺寸] 的本地化字符串。
        /// </summary>
        public static string Lang_ErrorImgSize => GetLanguage(SystemLangKeys.ErrorImgSize);

        /// <summary>
        ///   查找类似 [查找] 的本地化字符串。
        /// </summary>
        public static string Lang_Find => GetLanguage(SystemLangKeys.Find);

        /// <summary>
        ///   查找类似 [格式错误] 的本地化字符串。
        /// </summary>
        public static string Lang_FormatError => GetLanguage(SystemLangKeys.FormatError);

        /// <summary>
        ///   查找类似 [间隔10分钟] 的本地化字符串。
        /// </summary>
        public static string Lang_Interval10m => GetLanguage(SystemLangKeys.Interval10m);

        /// <summary>
        ///   查找类似 [间隔1小时] 的本地化字符串。
        /// </summary>
        public static string Lang_Interval1h => GetLanguage(SystemLangKeys.Interval1h);

        /// <summary>
        ///   查找类似 [间隔1分钟] 的本地化字符串。
        /// </summary>
        public static string Lang_Interval1m => GetLanguage(SystemLangKeys.Interval1m);

        /// <summary>
        ///   查找类似 [间隔2小时] 的本地化字符串。
        /// </summary>
        public static string Lang_Interval2h => GetLanguage(SystemLangKeys.Interval2h);

        /// <summary>
        ///   查找类似 [间隔30分钟] 的本地化字符串。
        /// </summary>
        public static string Lang_Interval30m => GetLanguage(SystemLangKeys.Interval30m);

        /// <summary>
        ///   查找类似 [间隔30秒] 的本地化字符串。
        /// </summary>
        public static string Lang_Interval30s => GetLanguage(SystemLangKeys.Interval30s);

        /// <summary>
        ///   查找类似 [间隔5分钟] 的本地化字符串。
        /// </summary>
        public static string Lang_Interval5m => GetLanguage(SystemLangKeys.Interval5m);

        /// <summary>
        ///   查找类似 [不能为空] 的本地化字符串。
        /// </summary>
        public static string Lang_IsNotEmpty => GetLanguage(SystemLangKeys.IsNotEmpty);

        /// <summary>
        ///   查找类似 [跳转] 的本地化字符串。
        /// </summary>
        public static string Lang_Jump => GetLanguage(SystemLangKeys.Jump);

        /// <summary>
        ///   查找类似 [查找类似 {0} 的本地化字符串。] 的本地化字符串。
        /// </summary>
        public static string Lang_LangComment => GetLanguage(SystemLangKeys.LangComment);

        /// <summary>
        ///   查找类似 [杂项] 的本地化字符串。
        /// </summary>
        public static string Lang_Miscellaneous => GetLanguage(SystemLangKeys.Miscellaneous);

        /// <summary>
        ///   查找类似 [下一页] 的本地化字符串。
        /// </summary>
        public static string Lang_NextPage => GetLanguage(SystemLangKeys.NextPage);

        /// <summary>
        ///   查找类似 [否] 的本地化字符串。
        /// </summary>
        public static string Lang_False => GetLanguage(SystemLangKeys.False);

        /// <summary>
        ///   查找类似 [暂无数据] 的本地化字符串。
        /// </summary>
        public static string Lang_NoData => GetLanguage(SystemLangKeys.NoData);

        /// <summary>
        ///   查找类似 [不在范围内] 的本地化字符串。
        /// </summary>
        public static string Lang_OutOfRange => GetLanguage(SystemLangKeys.OutOfRange);

        /// <summary>
        ///   查找类似 [页面模式] 的本地化字符串。
        /// </summary>
        public static string Lang_PageMode => GetLanguage(SystemLangKeys.PageMode);

        /// <summary>
        ///   查找类似 [下午] 的本地化字符串。
        /// </summary>
        public static string Lang_Pm => GetLanguage(SystemLangKeys.Pm);

        /// <summary>
        ///   查找类似 [PNG图片] 的本地化字符串。
        /// </summary>
        public static string Lang_PngImg => GetLanguage(SystemLangKeys.PngImg);

        /// <summary>
        ///   查找类似 [上一页] 的本地化字符串。
        /// </summary>
        public static string Lang_PreviousPage => GetLanguage(SystemLangKeys.PreviousPage);

        /// <summary>
        ///   查找类似 [滚动模式] 的本地化字符串。
        /// </summary>
        public static string Lang_ScrollMode => GetLanguage(SystemLangKeys.ScrollMode);

        /// <summary>
        ///   查找类似 [提示] 的本地化字符串。
        /// </summary>
        public static string Lang_Tip => GetLanguage(SystemLangKeys.Tip);

        /// <summary>
        ///   查找类似 [过大] 的本地化字符串。
        /// </summary>
        public static string Lang_TooLarge => GetLanguage(SystemLangKeys.TooLarge);

        /// <summary>
        ///   查找类似 [双页模式] 的本地化字符串。
        /// </summary>
        public static string Lang_TwoPageMode => GetLanguage(SystemLangKeys.TwoPageMode);

        /// <summary>
        ///   查找类似 [未知] 的本地化字符串。
        /// </summary>
        public static string Lang_Unknown => GetLanguage(SystemLangKeys.Unknown);

        /// <summary>
        ///   查找类似 [未知大小] 的本地化字符串。
        /// </summary>
        public static string Lang_UnknownSize => GetLanguage(SystemLangKeys.UnknownSize);

        /// <summary>
        ///   查找类似 [是] 的本地化字符串。
        /// </summary>
        public static string Lang_Yes => GetLanguage(SystemLangKeys.Yes);

        /// <summary>
        ///   查找类似 [放大] 的本地化字符串。
        /// </summary>
        public static string Lang_ZoomIn => GetLanguage(SystemLangKeys.ZoomIn);

        /// <summary>
        ///   查找类似 [缩小] 的本地化字符串。
        /// </summary>
        public static string Lang_ZoomOut => GetLanguage(SystemLangKeys.ZoomOut);
        #endregion
    }

    public enum SystemLangKeys
    {
        All,
        /// <summary>
        ///   查找类似 [上午] 的本地化字符串。
        /// </summary>
        Am,
        /// <summary>
        ///   查找类似 [取消] 的本地化字符串。
        /// </summary>
        Cancel,
        /// <summary>
        ///   查找类似 [清空] 的本地化字符串。
        /// </summary>
        Clear,
        /// <summary>
        ///   查找类似 [关闭] 的本地化字符串。
        /// </summary>
        Close,
        /// <summary>
        ///   查找类似 [关闭所有] 的本地化字符串。
        /// </summary>
        CloseAll,
        /// <summary>
        ///   查找类似 [关闭其他] 的本地化字符串。
        /// </summary>
        CloseOther,
        /// <summary>
        ///   查找类似 [确定] 的本地化字符串。
        /// </summary>
        Confirm,
        /// <summary>
        ///   查找类似 [错误的图片路径] 的本地化字符串。
        /// </summary>
        ErrorImgPath,
        /// <summary>
        ///   查找类似 [非法的图片尺寸] 的本地化字符串。
        /// </summary>
        ErrorImgSize,
        /// <summary>
        ///   查找类似 [查找] 的本地化字符串。
        /// </summary>
        Find,
        /// <summary>
        ///   查找类似 [格式错误] 的本地化字符串。
        /// </summary>
        FormatError,
        /// <summary>
        ///   查找类似 [间隔10分钟] 的本地化字符串。
        /// </summary>
        Interval10m,
        /// <summary>
        ///   查找类似 [间隔1小时] 的本地化字符串。
        /// </summary>
        Interval1h,
        /// <summary>
        ///   查找类似 [间隔1分钟] 的本地化字符串。
        /// </summary>
        Interval1m,
        /// <summary>
        ///   查找类似 [间隔2小时] 的本地化字符串。
        /// </summary>
        Interval2h,
        /// <summary>
        ///   查找类似 [间隔30分钟] 的本地化字符串。
        /// </summary>
        Interval30m,
        /// <summary>
        ///   查找类似 [间隔30秒] 的本地化字符串。
        /// </summary>
        Interval30s,
        /// <summary>
        ///   查找类似 [间隔5分钟] 的本地化字符串。
        /// </summary>
        Interval5m,
        /// <summary>
        ///   查找类似 [不能为空] 的本地化字符串。
        /// </summary>
        IsNotEmpty,
        /// <summary>
        ///   查找类似 [跳转] 的本地化字符串。
        /// </summary>
        Jump,
        /// <summary>
        ///   查找类似 [查找类似 {0} 的本地化字符串。] 的本地化字符串。
        /// </summary>
        LangComment,
        /// <summary>
        ///   查找类似 [杂项] 的本地化字符串。
        /// </summary>
        Miscellaneous,
        /// <summary>
        ///   查找类似 [下一页] 的本地化字符串。
        /// </summary>
        NextPage,
        /// <summary>
        ///   查找类似 [否] 的本地化字符串。
        /// </summary>
        False,
        /// <summary>
        ///   查找类似 [暂无数据] 的本地化字符串。
        /// </summary>
        NoData,
        /// <summary>
        ///   查找类似 [不在范围内] 的本地化字符串。
        /// </summary>
        OutOfRange,
        /// <summary>
        ///   查找类似 [页面模式] 的本地化字符串。
        /// </summary>
        PageMode,
        /// <summary>
        ///   查找类似 [下午] 的本地化字符串。
        /// </summary>
        Pm,
        /// <summary>
        ///   查找类似 [PNG图片] 的本地化字符串。
        /// </summary>
        PngImg,
        /// <summary>
        ///   查找类似 [上一页] 的本地化字符串。
        /// </summary>
        PreviousPage,
        /// <summary>
        ///   查找类似 [滚动模式] 的本地化字符串。
        /// </summary>
        ScrollMode,
        /// <summary>
        ///   查找类似 [提示] 的本地化字符串。
        /// </summary>
        Tip,
        /// <summary>
        ///   查找类似 [过大] 的本地化字符串。
        /// </summary>
        TooLarge,
        /// <summary>
        ///   查找类似 [双页模式] 的本地化字符串。
        /// </summary>
        TwoPageMode,
        /// <summary>
        ///   查找类似 [未知] 的本地化字符串。
        /// </summary>
        Unknown,
        /// <summary>
        ///   查找类似 [未知大小] 的本地化字符串。
        /// </summary>
        UnknownSize,
        /// <summary>
        ///   查找类似 [是] 的本地化字符串。
        /// </summary>
        Yes,
        /// <summary>
        ///   查找类似 [放大] 的本地化字符串。
        /// </summary>
        ZoomIn,
        /// <summary>
        ///   查找类似 [缩小] 的本地化字符串。
        /// </summary>
        ZoomOut,
    }

}