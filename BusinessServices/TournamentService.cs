using AutoMapper;
using TournamentManagementSystem.BusinessServices.BusinessInterfaces;
using TournamentManagementSystem.DTOs;
using TournamentManagementSystem.Entities;
using TournamentManagementSystem.Repositories;

namespace TournamentManagementSystem.BusinessServices
{
    public class TournamentService : ITournamentService
    {
        private readonly ISystemRepository _repo;
        private readonly IMapper _mapper;

        public TournamentService(ISystemRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TournamentDTO>> GetAllTournamentsAsync()
        {

            var tournamentEntities = await _repo.GetAllTournamentsAsync();
            var tournamentDTOs = _mapper.Map<IEnumerable<TournamentDTO>>(tournamentEntities);
            return tournamentDTOs;
        }

        public async Task<TournamentDTO> GetSingleTournamentAsync(int id)
        {
            var tournament =await GetTournamentOrThrowAsync(id);
            return _mapper.Map<TournamentDTO>(tournament);
        } 

        public async Task<TournamentDTO> CreateTournamentAsync(
            TournamentCreateDTO tournamentCreateDTO)
        {
            await EnsureUniqueAsync(tournamentCreateDTO.StartDate, tournamentCreateDTO.EndDate,
                tournamentCreateDTO.Name, tournamentCreateDTO.Location, tournamentCreateDTO.SportType);

            await EnsureOrganizerExistsOrThrowAsync(tournamentCreateDTO.OrganizerId);

            var tournamentForCreation = _mapper.Map<Tournament>(tournamentCreateDTO);
            await _repo.AddTournamentAsync(tournamentForCreation);

            return _mapper.Map<TournamentDTO>(tournamentForCreation);
        }


        public async Task UpdateTournamentAsync(
            TournamentUpdateDTO tournamentUpdateDTO, int id)
        {
            var currentTournament = await GetTournamentOrThrowAsync(id);

            await EnsureUniqueAsync(tournamentUpdateDTO.StartDate, tournamentUpdateDTO.EndDate,
                tournamentUpdateDTO.Name, tournamentUpdateDTO.Location, tournamentUpdateDTO.SportType, 
                id);

            if (tournamentUpdateDTO.OrganizerId != currentTournament.OrganizerId)
                await EnsureOrganizerExistsOrThrowAsync(tournamentUpdateDTO.OrganizerId);

            _mapper.Map(tournamentUpdateDTO, currentTournament);
            await _repo.UpdateTournamentAsync(currentTournament);
        }

        public async Task PatchTournamentAsync(
            TournamentPatchDTO patchedDTO, int id)
        {
            var tournamentEntity = await GetTournamentOrThrowAsync(id);

            var originalOrganizerId = tournamentEntity.OrganizerId;
            _mapper.Map(patchedDTO, tournamentEntity);
            //mapujemo odma zato sto nullable propertiji od patcheddto-a smetaju posle

            await EnsureUniqueAsync(tournamentEntity.StartDate, tournamentEntity.EndDate,
                tournamentEntity.Name, tournamentEntity.Location, tournamentEntity.SportType, id);

            if (patchedDTO.OrganizerId!.Value != originalOrganizerId)
                await EnsureOrganizerExistsOrThrowAsync(patchedDTO.OrganizerId!.Value);

            await _repo.UpdateTournamentAsync(tournamentEntity);

        }

        public async Task DeleteTournamentAsync(int id)
        {
            var tournament = await GetTournamentOrThrowAsync(id);

            // 2) (Optional) business rule, e.g. prevent deleting if teams/matches exist:

            await _repo.DeleteTournamentAsync(tournament);
        }

        private async Task<Tournament> GetTournamentOrThrowAsync(int id)
        {
            return await _repo.GetTournamentAsync(id)
                ?? throw new KeyNotFoundException($"Tournament {id} not found.");
        }
        private async Task EnsureUniqueAsync(DateTime start, DateTime end,
            string name, string location, string sportType, int? excludeId = null)
        {
            //uniqueness check
            if (await _repo.TournamentExistsAsync(start, end, name, location, sportType, excludeId))
            {
                throw new InvalidOperationException(
                    "Another tournament with those details already exists.");
            }
        }

        //da se ne zeznes ovo je tu zbog FK-a nije greska
        private async Task EnsureOrganizerExistsOrThrowAsync(int organizerId)
        {
            //ako organizer ne postoji baci exception
            if (!await _repo.OrganizerExistsAsync(organizerId))
                throw new KeyNotFoundException($"Organizer {organizerId} not found.");
        }
    }
}
