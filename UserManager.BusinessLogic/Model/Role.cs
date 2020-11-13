using System;

namespace UserManager.BusinessLogic.Model
{
    public class Role
    {
        private Role(int id, string name, string description)
        {
            Id = id;
            Name = name;
            Description = description;
        }

        public int Id { get; }

        public string Name { get; }

        public string Description { get; }

        public static Role Admin { get; } = new Role(1, "Admin", "Role which can perform elevated actions");

        public static Role Basic { get; } = new Role(2, "Basic", "Role which can only read info");

        public static Role[] Roles { get; } = new Role[] { Admin, Basic };

        public static Role FromId(int id)
        {
            if (id == Admin.Id) return Admin;
            if (id == Basic.Id) return Basic;
            
            throw new ArgumentException($"Invalid value {id}", nameof(id));
        }
    }
}
