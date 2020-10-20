﻿using System.Text.RegularExpressions;

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
            Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            Match match = regex.Match(email);
            if (!match.Success)
            {
                throw new ValidationException($"Invalid Email: {email}");
            }
        }
    }
}