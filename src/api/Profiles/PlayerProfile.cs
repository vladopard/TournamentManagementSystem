using AutoMapper;
using static TournamentManagementSystem.DTOs.Player.PlayerBaseDTO;
using TournamentManagementSystem.Entities;
using TournamentManagementSystem.DTOs.Player;

namespace TournamentManagementSystem.Profiles
{
    public class PlayerProfile : Profile
    {
        public PlayerProfile() {
            CreateMap<Player, PlayerDTO>()
                .ForMember(dest => dest.TeamName,
                opt => opt.MapFrom(src => src.Team.Name))
                .ReverseMap();
            CreateMap<PlayerCreateDTO, Player>();
            CreateMap<PlayerUpdateDTO, Player>();
            CreateMap<PlayerDTO, PlayerPatchDTO>();
            CreateMap<PlayerPatchDTO, Player>();
        }
    }
}
