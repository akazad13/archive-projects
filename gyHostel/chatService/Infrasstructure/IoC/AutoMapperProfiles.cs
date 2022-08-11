using AutoMapper;
using chatService.DTO;
using DataAccess.Models;

namespace userService.Infrasstructure.IoC
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<ChatDTO, Chat>();
        }
    }
}
