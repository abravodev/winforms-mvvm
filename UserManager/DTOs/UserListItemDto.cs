using System.ComponentModel;

namespace UserManager.DTOs
{
    public class UserListItemDto
    {
        [Browsable(false)]
        public int Id { get; set; }

        [DisplayName("First name")]
        public string FirstName { get; set; }

        [DisplayName("Last name")]
        public string LastName { get; set; }

        [Browsable(false)]
        public string Fullname => $"{FirstName} {LastName}";

        [DisplayName("Email")]
        public string Email { get; set; }

        [DisplayName("Role")]
        public string Role { get; set; }
    }
}
