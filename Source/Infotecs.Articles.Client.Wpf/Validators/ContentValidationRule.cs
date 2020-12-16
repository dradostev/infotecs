using System.Globalization;
using System.Windows.Controls;

namespace Infotecs.Articles.Client.Wpf.Validators
{
    /// <summary>
    /// Validation rule for Text Content.
    /// </summary>
    public class ContentValidationRule : ValidationRule
    {
        /// <inheritdoc/>
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            var isValid = ((string)value).Length >= 10;
            return new ValidationResult(isValid, "Should contain 10 or more characters");
        }
    }
}
