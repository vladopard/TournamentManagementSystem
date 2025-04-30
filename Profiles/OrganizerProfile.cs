using AutoMapper;
using TournamentManagementSystem.DTOs.Organizer;
using TournamentManagementSystem.DTOs.Tournament;
using TournamentManagementSystem.Entities;

namespace TournamentManagementSystem.Profiles
{
    public class OrganizerProfile : Profile
    {
        public OrganizerProfile() 
        {
            CreateMap<Organizer, OrganizerDTO>().ReverseMap();
            CreateMap<OrganizerUpdateDTO, Organizer>();
            CreateMap<OrganizerCreateDTO, Organizer>();
            CreateMap<OrganizerDTO, OrganizerPatchDTO>();
            CreateMap<OrganizerPatchDTO,Organizer>();
        }
    }
}
