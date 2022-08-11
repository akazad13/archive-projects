using AutoMapper;
using TakeItToTheCloud.Models;
using TakeItToTheCloud.Models.Dto;

namespace TakeItToTheCloud.Infrastructure
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {

            CreateMap<UserForRegisterDto, User>()
                .ForMember(dest => dest.UserName, opt =>
                    opt.MapFrom(src => src.Email));
            CreateMap<UploadedFile, UploadedFileDetails>()
                .ForMember(dest => dest.FirstName, opt =>
                    opt.MapFrom(src => src.User.FirstName))
                .ForMember(dest => dest.LastName, opt =>
                    opt.MapFrom(src => src.User.LastName));
        }
    }
}
