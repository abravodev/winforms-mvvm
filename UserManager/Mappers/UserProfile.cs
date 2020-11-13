using AutoMapper;
using UserManager.BusinessLogic.Model;
using UserManager.DTOs;

namespace UserManager.Mappers
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserListItemDto>()
                .ForMember(x => x.Email, x => x.MapFrom(u => ShowEmail(u)))
                .ForMember(x => x.Role, x => x.MapFrom(u => u.Role.Name));

            CreateMap<CreateUserDto, User>()
                .ForMember(x => x.Email, x => x.MapFrom(u => new Email(u.Email)));
        }

        private static string ShowEmail(User u) => u.Email?.ToString() ?? string.Empty;
    }
}
