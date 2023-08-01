using AutoMapper;
using Ticket_Management_System_API.Models;
using Ticket_Management_System_API.Models.Dto;

namespace Ticket_Management_System_API.Profiles
{
    public class EventProfile : Profile
    {
        public EventProfile()
        {
            CreateMap<Event,EventDto>().ReverseMap();
        }
    }
}
