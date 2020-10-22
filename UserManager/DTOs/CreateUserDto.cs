using System.ComponentModel.DataAnnotations;
using MvvmTools.Bindings;

namespace UserManager.DTOs
{
    public class CreateUserDto : BindableObject
    {
        private string _firstName;
        [Required]
        public string FirstName
        {
            get { return _firstName; }
            set { SetProperty(ref _firstName, value); }
        }

        private string _lastName;
        [Required]
        public string LastName
        {
            get { return _lastName; }
            set { SetProperty(ref _lastName, value); }
        }

        private string _email;
        [Required]
        public string Email
        {
            get { return _email; }
            set { SetProperty(ref _email, value); }
        }
    }
}
