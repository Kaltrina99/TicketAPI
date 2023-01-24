using AutoMapper;
using Domain;

namespace Application.Tickets
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<Ticket,TicketDto>();
            CreateMap<UserTicket,AssignedDto>()
            .ForMember(d=>d.Username, o=>o.MapFrom(s=>s.User.UserName))
            .ForMember(d=>d.DisplayName, o=>o.MapFrom(s=>s.User.DisplayName));
        }
    }
}