using System.Xml.Linq;
using AutoMapper;
using TournamentManagementSystem.BusinessServices.BusinessInterfaces;
using TournamentManagementSystem.DTOs.Player;
using TournamentManagementSystem.Entities;
using TournamentManagementSystem.Repositories;

namespace TournamentManagementSystem.BusinessServices
{
    public class PlayerService : IPlayerService
    {
        private readonly ISystemRepository _repo;
        private readonly IMapper _mapper;

        public PlayerService(ISystemRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PlayerDTO>> GetAllPlayersAsync()
        {
            var playerEntities = await _repo.GetAllPlayersAsync();
            return _mapper.Map<IEnumerable<PlayerDTO>>(playerEntities);
        }
        public async Task<PlayerDTO> GetPlayerAsync(int id)
        {
            var playerEntity = await GetPlayerOrThrowAsync(id);
            return _mapper.Map<PlayerDTO>(playerEntity);
        }
        public async Task<PlayerDTO> AddPlayerAsync(PlayerCreateDTO playerCreateDTO)
        {
            await EnsureTeamFKExistsOrThrowAsync(playerCreateDTO.TeamId);

            await EnsureUniquePlayer(playerCreateDTO.FirstName, playerCreateDTO.LastName,
                playerCreateDTO.DateOfBirth);

            var playerEntity = _mapper.Map<Player>(playerCreateDTO);
            await _repo.AddPlayerAsync(playerEntity);
            return _mapper.Map<PlayerDTO>(playerEntity);
        }
        public async Task UpdatePlayerAsync(PlayerUpdateDTO playerUpdateDTO, int id)
        {
            var playerEntity = await GetPlayerOrThrowAsync(id);

            if(playerEntity.TeamId != playerUpdateDTO.TeamId)
                await EnsureTeamFKExistsOrThrowAsync(playerUpdateDTO.TeamId);

            await EnsureUniquePlayer(playerUpdateDTO.FirstName, playerUpdateDTO.LastName,
                playerUpdateDTO.DateOfBirth, id);

            _mapper.Map(playerUpdateDTO, playerEntity);
            await _repo.UpdatePlayerAsync(playerEntity);
        }

        public async Task PatchPlayerAsync(PlayerPatchDTO patchedDTO, int id)
        {
            var playerEntity = await GetPlayerOrThrowAsync(id);

            if (patchedDTO.TeamId.HasValue &&
                patchedDTO.TeamId.Value != playerEntity.TeamId)
                await EnsureTeamFKExistsOrThrowAsync(patchedDTO.TeamId.Value);

            _mapper.Map(patchedDTO, playerEntity);

            await EnsureUniquePlayer(playerEntity.FirstName, playerEntity.LastName,
                playerEntity.DateOfBirth, id);

            await _repo.UpdatePlayerAsync(playerEntity);
        }
        public async Task DeletePlayerAsync(int id)
        {
            var player = await GetPlayerOrThrowAsync(id);
            await _repo.DeletePlayerAsync(player);
        }

        //HELPERI HELPERI HELPERI
        private async Task<Player> GetPlayerOrThrowAsync(int id)
        {
            return await _repo.GetPlayerAsync(id)
                ?? throw new KeyNotFoundException($"Player with id: {id} not found");
        }
        private async Task EnsureTeamFKExistsOrThrowAsync(int teamId)
        {
            if(!await _repo.TeamFkExistsAsync(teamId))
                throw new KeyNotFoundException($"Team with id: {teamId} not found");
        }
        private async Task EnsureUniquePlayer(string firstName, string lastName,
            DateTime dob, int? excludeId = null)
        {
            if(await _repo.PlayerExistsAsync(firstName, lastName, dob, excludeId))
                throw new InvalidOperationException
                    ($"A player named '{firstName + " " + lastName}' already exists in");
        }

    }
}
