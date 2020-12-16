using System.Globalization;
using System.Windows.Controls;

namespace Infotecs.Articles.Client.Wpf.Validators
{
    /// <summary>
    /// Validation rule for Username.
    /// </summary>
    public class UsernameValidationRule : ValidationRule
    {
        /// <inheritdoc/>
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            var isValid = ((string)value).Length >= 3;
            return new ValidationResult(isValid, "Should contain 3 or more characters");
        }
    }
}
