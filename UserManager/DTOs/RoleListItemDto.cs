using MvvmTools.Bindings;

namespace UserManager.DTOs
{
    public class RoleListItemDto : BindableObject
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
    }
}