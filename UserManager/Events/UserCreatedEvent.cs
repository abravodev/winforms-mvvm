using UserManager.BusinessLogic.Model;

namespace UserManager.Events
{
    public class UserCreatedEvent
    {
        public UserCreatedEvent(User createdUser)
        {
            CreatedUser = createdUser;
        }

        public User CreatedUser { get; }
    }
}