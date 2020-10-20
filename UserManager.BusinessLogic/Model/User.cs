namespace UserManager.BusinessLogic.Model
{
    public class User
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public Email Email { get; set; }
    }
}
