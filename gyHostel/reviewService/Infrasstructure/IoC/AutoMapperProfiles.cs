using AutoMapper;
using DataAccess.Models;
using reviewService.DTO;

namespace userService.Infrasstructure.IoC
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<ReviewDTO, Review>();
        }
    }
}
