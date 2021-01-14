using System;

namespace UserManager.BusinessLogic.Model
{
    public class User
    {
        public User()
        {
            this.CreationDate = DateTimeOffset.Now;
        }

        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public Email Email { get; set; }

        public Role Role { get; set; }

        public DateTimeOffset CreationDate { get; set; }
    }
}