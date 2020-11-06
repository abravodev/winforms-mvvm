using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace UserManager.BusinessLogic.Model
{
    public class Email
    {
        private readonly string _email;

        public Email(string email)
        {
            new EmailValidator().Validate(email);
            _email = email;
        }

        public override string ToString() => _email;
    }

    public class EmailValidator
    {
        public void Validate(string email)
        {
            if (!IsValid(email))
            {
                throw new ValidationException($"Invalid Email: {email}");
            }
        }

        public bool IsValid(string email)
        {
            Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            Match match = regex.Match(email);
            return match.Success;
        }
    }

    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class EmailAttribute : ValidationAttribute
    {
        private static readonly EmailValidator _validator = new EmailValidator();

        public override bool IsValid(object value)
        {
            var email = value as string;
            if (email == null)
            {
                return true; // This should be handled by Required attribute
            }

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