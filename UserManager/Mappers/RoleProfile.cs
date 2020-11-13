using AutoMapper;
using UserManager.BusinessLogic.Model;
using UserManager.DTOs;

namespace UserManager.Mappers
{
    public class RoleProfile : Profile
    {
        public RoleProfile()
        {
            CreateMap<Role, RoleListItemDto>();
            CreateMap<Role, RoleSelectDto>();
            CreateMap<RoleSelectDto, Role>()
                .ConstructUsing(x => Role.FromId(x.Id));
        }
    }
}
