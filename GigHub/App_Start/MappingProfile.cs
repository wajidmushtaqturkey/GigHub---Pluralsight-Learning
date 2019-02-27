using AutoMapper;
using GigHub.Dtos;
using GigHub.Models;

namespace GigHub.App_Start
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            Mapper.Map<UserDto>(new ApplicationUser());
            Mapper.Map<GigDto>(new Gig());
            Mapper.Map<NotificationDto>(new Notification());
        }
    }
}