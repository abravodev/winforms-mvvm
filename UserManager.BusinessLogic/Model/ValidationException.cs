using System;

namespace UserManager.BusinessLogic.Model
{
    public class ValidationException : Exception
    {
        public ValidationException(string message) : base(message)
        {

        }
    }
}