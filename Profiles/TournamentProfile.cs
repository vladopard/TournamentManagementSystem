using AutoMapper;
using TournamentManagementSystem.DTOs;
using TournamentManagementSystem.DTOs.Tournament;
using TournamentManagementSystem.Entities;

namespace TournamentManagementSystem.Profiles
{
    public class TournamentProfile : Profile
    {
        public TournamentProfile() {

            CreateMap<Tournament, TournamentDTO>()
                .ForMember(dest => dest.OrganizerName,
                opt => opt.MapFrom(src => src.Organizer.Name))
                .ForMember(dest => dest.OrganizerId,
                opt => opt.MapFrom(src => src.Organizer.OrganizerId))
                .ForMember(dest => dest.TeamNames,
                opt => opt.MapFrom(src => src.Teams.Select(t => t.Name)))
                .ReverseMap();
            CreateMap<TournamentCreateDTO, Tournament>();
            CreateMap<TournamentUpdateDTO, Tournament>();
            CreateMap<TournamentDTO, TournamentPatchDTO>();
            CreateMap<TournamentPatchDTO, Tournament>();
        }
    }
}
