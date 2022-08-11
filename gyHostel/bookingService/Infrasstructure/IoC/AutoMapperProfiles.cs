using AutoMapper;
using bookingService.DTO;
using DataAccess.Models;

namespace userService.Infrasstructure.IoC
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<BookingDTO, Booking>();
        }
    }
}
