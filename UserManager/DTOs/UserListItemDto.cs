using System.ComponentModel;
using System.Windows.Forms;
using WinformsTools.MVVM.Bindings;

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
        [TableColumn(AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells)]
        public string Role { get; set; }

        [DisplayName("Creation Date")]
        [TableColumn(AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells)]
        public string CreationDate { get; set; }
    }
}
