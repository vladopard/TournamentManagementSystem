using AutoMapper;
using TournamentManagementSystem.DTOs.PlayerStats;
using TournamentManagementSystem.Entities;

namespace TournamentManagementSystem.Profiles
{
    public class PlayerMatchStatsProfile : Profile
    {
        public PlayerMatchStatsProfile() 
        {
            CreateMap<PlayerMatchStats, PlayerMatchStatsDTO>()
                .ForMember(dest => dest.MatchDate,
                    opt => opt.MapFrom(source => source.Match.StartDate))
                .ForMember(dest => dest.Opponent,
                    opt => opt.MapFrom(source =>
                    source.Match.HomeTeamId == source.Player.TeamId
                    ? source.Match.AwayTeam.Name
                    : source.Match.HomeTeam.Name));
        
        }
    }
}
