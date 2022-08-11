using AutoMapper;
using CMS.Models;
using CMS.Models.Dto;

namespace CMS.Infrastructure
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {

            CreateMap<UserForRegisterDto, User>();
            CreateMap<UserForUpdateDto, User>();
            CreateMap<CourseForUpdateDto, Course>();
            CreateMap<CourseForAddDto, Course>();
            CreateMap<QuizForAddDto, Quiz>();
            CreateMap<QuizForUpdateDto, Quiz>();
        }
    }
}
