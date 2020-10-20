using System.ComponentModel;
using UserManager.BusinessLogic.Model;

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

        [DisplayName("Email")]
        public string Email { get; set; }

        public static UserListItemDto Map(User user)
        {
            return new UserListItemDto
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email?.ToString() ?? "-"
            };
        }
    }
}
