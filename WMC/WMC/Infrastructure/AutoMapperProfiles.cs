using AutoMapper;
using WMC.Models;
using WMC.Models.Dto;

namespace WMC.Infrastructure
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {

            CreateMap<UserForRegisterDto, User>()
                .ForMember(dest => dest.Name, opt =>
                    opt.MapFrom(src => src.CustomerName))
                .ForMember(dest => dest.UserName, opt =>
                    opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.PhoneNumber, opt =>
                    opt.MapFrom(src => src.ContractNumber));

            CreateMap<AddStaffDto, UserForRegisterDto>()
                .ForMember(dest => dest.CustomerName, opt =>
                    opt.MapFrom(src => src.Name));

            CreateMap<AddConsultationDto, Consultation>();
            CreateMap<UpdateConsultationDto, Consultation>();
            CreateMap<AddFeedbackDto, Feedback>();
        }
    }
}
