using UserManager.BusinessLogic.Model;

namespace UserManager.BusinessLogic.DataAccess.Mappers
{
    public class RoleMapper : IntTypeHandler<Role>
    {
        protected override int Format(Role role) => role.Id;

        protected override Role Parse(int roleId) => Role.FromId(roleId);
    }
}
