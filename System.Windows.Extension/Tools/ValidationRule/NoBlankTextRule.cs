using System.Globalization;
using System.Windows.Controls;
using System.Windows.Extension.Langs;

namespace System.Windows.Extension.Tools
{
    public class NoBlankTextRule : ValidationRule
    {
        public string ErrorContent { get; set; } = Lang.CurrentLanguage.Lang_IsNotEmpty;

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (value is not string text)
            {
                return new ValidationResult(false, Lang.CurrentLanguage.Lang_FormatError);
            }

            if (string.IsNullOrEmpty(text))
            {
                return new ValidationResult(false, ErrorContent);
            }

            return ValidationResult.ValidResult;
        }
    }
}