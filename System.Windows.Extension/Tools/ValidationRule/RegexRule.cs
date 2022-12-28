using System.Globalization;
using System.Windows.Controls;
using System.Windows.Extension.Data;
using System.Windows.Extension.Langs;

namespace System.Windows.Extension.Tools
{
    public class RegexRule : ValidationRule
    {
        public TextType Type { get; set; }

        public string Pattern { get; set; }

        public string ErrorContent { get; set; } = Lang.CurrentLanguage.Lang_FormatError;

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (value is not string text)
            {
                return CreateErrorValidationResult();
            }

            if (!string.IsNullOrEmpty(Pattern))
            {
                if (!text.IsKindOf(Pattern))
                {
                    return CreateErrorValidationResult();
                }
            }
            else if (Type != TextType.Common)
            {
                if (!text.IsKindOf(Type))
                {
                    return CreateErrorValidationResult();
                }
            }

            return ValidationResult.ValidResult;
        }

        private ValidationResult CreateErrorValidationResult()
        {
            return new ValidationResult(false, ErrorContent);
        }
    }
}