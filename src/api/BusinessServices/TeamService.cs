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

        public async Task<TeamDTO> AddTeamAsync(TeamCreateDTO teamCreateDTO)
        {
            await EnsureTournamentFKExistsOrThrowAsync(teamCreateDTO.TournamentId);

            await EnsureUniqueTeamAsync(teamCreateDTO.Name, teamCreateDTO.TournamentId);

            var teamEntity = _mapper.Map<Team>(teamCreateDTO);
            await _repo.AddTeamAsync(teamEntity);
            return _mapper.Map<TeamDTO>(teamEntity);
        }
        public async Task UpdateTeamAsync(TeamUpdateDTO teamUpdateDTO, int id)
        {
            var teamEntity = await GetTeamOrThrowAsync(id);

            if(teamUpdateDTO.TournamentId !=  teamEntity.TournamentId)
                await EnsureTournamentFKExistsOrThrowAsync(teamUpdateDTO.TournamentId);

            await EnsureUniqueTeamAsync(teamUpdateDTO.Name, teamUpdateDTO.TournamentId, id);

            _mapper.Map(teamUpdateDTO, teamEntity);
            await _repo.UpdateTeamAsync(teamEntity);
        }

        public async Task PatchTeamAsync(TeamPatchDTO patchedDTO, int id)
        {
            var teamEntity = await GetTeamOrThrowAsync(id);

            if (patchedDTO.TournamentId.HasValue &&
                patchedDTO.TournamentId.Value != teamEntity.TournamentId)
                await EnsureTournamentFKExistsOrThrowAsync(patchedDTO.TournamentId.Value);

            _mapper.Map(patchedDTO, teamEntity);

            await EnsureUniqueTeamAsync(teamEntity.Name, teamEntity.TournamentId, id);

            await _repo.UpdateTeamAsync(teamEntity);
        }

        public async Task DeleteTeamAsync(int id)
        {
            var team = await GetTeamOrThrowAsync(id);

            if(await _repo.TeamHasPlayersAsync(id))
                throw new InvalidOperationException(
                "Cannot delete team while players are still assigned.");

            if (await _repo.TeamHasMatchesAsync(id))
                throw new InvalidOperationException(
                    "Cannot delete team while matches still reference it.");

            await _repo.DeleteTeamAsync(team);
        }

        //////HELPERI
        //////HELPERI
        //////HELPERI
        
        private async Task<Team> GetTeamOrThrowAsync(int id)
        {
            return await _repo.GetTeamAsync(id)
                ?? throw new KeyNotFoundException($"Team with id: {id} not found");   
        }

        private async Task EnsureUniqueTeamAsync(string name, int tournamentId, int? excludeId = null)
        {
            if (await _repo.TeamExistsAsync(name, tournamentId, excludeId))
                throw new InvalidOperationException
                    ($"A team named '{name}' already exists in tournament {tournamentId}");
        }

        private async Task EnsureTournamentFKExistsOrThrowAsync(int tournamentId)
        {
            if (!await _repo.TournamentFKExistsAsync(tournamentId))
                throw new KeyNotFoundException($"Tournament with the id {tournamentId} doesn't exist");
        }



    }
}
