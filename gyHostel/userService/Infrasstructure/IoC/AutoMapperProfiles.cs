using AutoMapper;
using DataAccess.Models;
using userService.DTO;

namespace userService.Infrasstructure.IoC
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<UserDTO, User>();
        }
    }
}
