using System.ComponentModel.DataAnnotations;
using WinformsTools.MVVM.Bindings;
using UserManager.BusinessLogic.Model;

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
        [Email]
        public string Email
        {
            get { return _email; }
            set { SetProperty(ref _email, value); }
        }

        private RoleSelectDto _roleId;
        [Required]
        public RoleSelectDto Role
        {
            get { return _roleId; }
            set { SetProperty(ref _roleId, value); }
        }
    }
}
