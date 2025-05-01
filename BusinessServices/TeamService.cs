using AutoMapper;
using TournamentManagementSystem.DTOs;
using TournamentManagementSystem.DTOs.Team;
using TournamentManagementSystem.Entities;
using TournamentManagementSystem.Repositories;

namespace TournamentManagementSystem.BusinessServices
{
    public class TeamService : ITeamService
    {
        private readonly ISystemRepository _repo;
        private readonly IMapper _mapper;

        public TeamService(ISystemRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TeamDTO>> GetAllTeamsAsync()
        {
            var tournamentEntities = await _repo.GetAllTeamsAsync();
            return _mapper.Map<IEnumerable<TeamDTO>>(tournamentEntities);
        }
        public async Task<TeamDTO> GetTeamAsync(int id)
        {
            var teamEntity = await GetTeamOrThrowAsync(id);
            return _mapper.Map<TeamDTO>(teamEntity);
        }

        //////HELPERI
        //////HELPERI
        //////HELPERI
        
        private async Task<Team> GetTeamOrThrowAsync(int id)
        {
            return await _repo.GetTeamAsync(id)
                ?? throw new KeyNotFoundException($"Team with id: {id} not found");   
        }

    }
}
