using Dapper.Contrib.Extensions;
using System;

namespace UserManager.BusinessLogic.Model
{
    [Table("[User]")]
    public class User
    {
        public User()
        {
            this.CreationDate = DateTimeOffset.Now;
        }

        [Key]
        public int Id { get; private set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public Email Email { get; set; }

        public Role Role { get; set; }

        public DateTimeOffset CreationDate { get; }
    }
}