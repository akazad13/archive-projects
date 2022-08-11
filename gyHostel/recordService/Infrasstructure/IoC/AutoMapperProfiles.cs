using AutoMapper;
using DataAccess.Models;
using recordService.DTO;

namespace userService.Infrasstructure.IoC
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<RecordDTO, Record>();
        }
    }
}
