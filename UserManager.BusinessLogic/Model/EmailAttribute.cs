using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace UserManager.BusinessLogic.Model
{

    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class EmailAttribute : ValidationAttribute
    {
        private static readonly EmailValidator _validator = new EmailValidator();

        public override bool IsValid(object value)
        {
            if (value == null)
            {
                return true; // This should be handled by Required attribute
            }

            if(!(value is string))
            {
                throw new ArgumentException($"string type required. Found {value.GetType()}", nameof(value));
            }

            var email = value as string;
            return _validator.IsValid(email);
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var email = value as string;
            if(email != null)
            {
                var isValid = _validator.IsValid(email);
                if (!isValid)
                {
                    return new ValidationResult($"Invalid Email: {email}");
                }
            }

            return base.IsValid(value, validationContext);
        }
    }
}