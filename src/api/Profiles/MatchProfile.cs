using AutoMapper;
using TournamentManagementSystem.DTOs.Match;
using TournamentManagementSystem.Entities;

namespace TournamentManagementSystem.Profiles
{
    public class MatchProfile : Profile
    {
        public MatchProfile() {
            CreateMap<Match, MatchDTO>()
                .ForMember(dest => dest.HomeTeamName,
                           opt => opt.MapFrom(src => src.HomeTeam.Name))
                .ForMember(dest => dest.AwayTeamName,
                           opt => opt.MapFrom(src => src.AwayTeam.Name))
                .ReverseMap();

            CreateMap<MatchCreateDTO, Match>();
            CreateMap<MatchUpdateDTO, Match>();
            CreateMap<MatchDTO, MatchPatchDTO>();
            CreateMap<MatchPatchDTO, Match>();
            
        }
    }
}
