using AutoMapper;
using DataAccess.Models;
using roomService.DTO;

namespace userService.Infrasstructure.IoC
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<RoomDTO, Room>();
        }
    }
}
