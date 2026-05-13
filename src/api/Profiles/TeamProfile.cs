using AutoMapper;
using TournamentManagementSystem.DTOs.Team;
using TournamentManagementSystem.Entities;

namespace TournamentManagementSystem.Profiles
{
    public class TeamProfile : Profile
    {
        public TeamProfile() {
            CreateMap<Team, TeamDTO>()
                .ForMember(dest => dest.TournamentName,
                opt => opt.MapFrom(src => src.Tournament.Name))
                .ReverseMap();

            CreateMap<TeamCreateDTO, Team>();
            CreateMap<TeamUpdateDTO, Team>();
            CreateMap<TeamDTO, TeamPatchDTO>();
            CreateMap<TeamPatchDTO, Team>();
        }
    }
}
