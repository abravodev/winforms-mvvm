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

        /// <summary>
        /// Validate email based on <see cref="EmailAddressAttribute"/>
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public bool IsValid(string email)
        {
            return new EmailAddressAttribute().IsValid(email);
        }
    }
}