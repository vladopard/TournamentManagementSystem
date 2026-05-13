using AutoMapper;
using TournamentManagementSystem.BusinessServices.BusinessInterfaces;
using TournamentManagementSystem.DTOs.PlayerStats;
using TournamentManagementSystem.Entities;
using TournamentManagementSystem.Repositories;

namespace TournamentManagementSystem.BusinessServices
{
    public class PlayerMatchStatsService : IPlayerMatchStatsService
    {
        private readonly ISystemRepository _repo;
        private readonly IMapper _mapper;

        public PlayerMatchStatsService(ISystemRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PlayerMatchStatsDTO>> GetAllStatsForPlayerAsync(int playerId)
        {
            var statsForPlayer = await _repo.GetAllStatsForPlayerAsync(playerId);
            return _mapper.Map<IEnumerable<PlayerMatchStatsDTO>>(statsForPlayer);
        }

        public async Task<PlayerMatchStatsDTO> 
            GetStatsForPlayerFromOneMatchAsync(int playerId, int matchId)
        {
            var statsEntity = await _repo.GetStatsForPlayerFromOneMatchAsync(playerId, matchId);
            if (statsEntity is null) throw new KeyNotFoundException
                    ($"Stats for match {matchId} and player {playerId} not found.");

            return _mapper.Map<PlayerMatchStatsDTO>(statsEntity);
        }

    }
}
