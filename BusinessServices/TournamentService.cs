using AutoMapper;
using TournamentManagementSystem.BusinessServices.BusinessInterfaces;
using TournamentManagementSystem.DTOs;
using TournamentManagementSystem.DTOs.Tournament;
using TournamentManagementSystem.Entities;
using TournamentManagementSystem.Repositories;

namespace TournamentManagementSystem.BusinessServices
{
    public class TournamentService : ITournamentService
    {
        private readonly ITournamentRepository _repo;
        private readonly IMapper _mapper;

        public TournamentService(ITournamentRepository repo, IMapper mapper)
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

        public async Task<TournamentDTO?> GetSingleTournamentAsync(int id)
        {
            var tournament = await _repo.GetTournament(id);
            if (tournament == null) return null;
            return _mapper.Map<TournamentDTO>(tournament);
        } 

        public async Task<TournamentDTO?> CreateTournamentAsync(
            TournamentCreateDTO tournamentCreateDTO)
        {
            var tournamentForCreation = _mapper.Map<Tournament>(tournamentCreateDTO);
            //add organizer check later

            var createdTournamentEntity = await _repo.AddTournamentAsync(tournamentForCreation);
            if (createdTournamentEntity == null) return null;

            return _mapper.Map<TournamentDTO>(createdTournamentEntity);
        }

        public async Task<bool> UpdateTournamentAsync(
            TournamentUpdateDTO tournamentUpdateDTO, int id)
        {
            //add organizer check later
            var currentTournament = await _repo.GetTournament(id);
            if (currentTournament == null) return false;

            _mapper.Map(tournamentUpdateDTO, currentTournament);
            return await _repo.SaveChangesAsync();
        }

        public async Task<bool> PatchTournamentAsync(
            TournamentPatchDTO patchedDTO, int id)
        {
            var tournamentEntity = await _repo.GetTournament(id);
            if (tournamentEntity == null) return false;

            //add organizer check later
            _mapper.Map(patchedDTO, tournamentEntity);
            return await _repo.SaveChangesAsync();

        }

        public async Task<bool> DeleteTournamentAsync(int id)
        {
            return await _repo.DeleteTournamentAsync(id);
        }
    }
}
