using System.Xml.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TournamentManagementSystem.BusinessServices.BusinessInterfaces;
using TournamentManagementSystem.DTOs.Match;
using TournamentManagementSystem.Entities;
using TournamentManagementSystem.Repositories;

namespace TournamentManagementSystem.BusinessServices
{
    public class MatchService : IMatchService
    {
        private readonly ISystemRepository _repo;
        private readonly IMapper _mapper;

        public MatchService(ISystemRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<IEnumerable<MatchDTO>> GetMatchesAsync()
        {
            var matches = await _repo.GetAllMatchesAsync();
            return _mapper.Map<IEnumerable<MatchDTO>>(matches);
        }

        public async Task<MatchDTO> GetMatchAsync(int id)
        {
            var matchEntity = await GetMatchOrThrowAsync(id);
            return _mapper.Map<MatchDTO>(matchEntity);
        }

        public async Task<MatchDTO> AddMatchAsync(MatchCreateDTO matchCreateDTO)
        {
            await EnsureTournamentFKExistsOrThrowAsync(matchCreateDTO.TournamentId);
            await EnsureTeamFKExistsOrThrowAsync(matchCreateDTO.HomeTeamId);
            await EnsureTeamFKExistsOrThrowAsync(matchCreateDTO.AwayTeamId);

            await EnsureUniqueMatchAsync(matchCreateDTO.StartDate, matchCreateDTO.EndDate,
                matchCreateDTO.HomeTeamId, matchCreateDTO.AwayTeamId);

            await EnsureTeamIsAvailableAsync(matchCreateDTO.HomeTeamId, matchCreateDTO.StartDate, matchCreateDTO.EndDate);
            await EnsureTeamIsAvailableAsync(matchCreateDTO.AwayTeamId, matchCreateDTO.StartDate, matchCreateDTO.EndDate);

            var matchEntity = _mapper.Map<Match>(matchCreateDTO);
            await _repo.AddMatchAsync(matchEntity);
            return _mapper.Map<MatchDTO>(matchEntity);
        }

        public async Task UpdateMatchAsync(MatchUpdateDTO matchUpdateDTO, int id)
        {
            var matchEntity = await GetMatchOrThrowAsync(id);

            if (matchUpdateDTO.TournamentId != matchEntity.TournamentId)
                await EnsureTournamentFKExistsOrThrowAsync(matchUpdateDTO.TournamentId);
            if (matchUpdateDTO.HomeTeamId != matchEntity.HomeTeamId)
                await EnsureTeamFKExistsOrThrowAsync(matchUpdateDTO.HomeTeamId);
            if (matchUpdateDTO.AwayTeamId != matchEntity.AwayTeamId)
                await EnsureTeamFKExistsOrThrowAsync(matchUpdateDTO.AwayTeamId);

            await EnsureUniqueMatchAsync(matchUpdateDTO.StartDate, matchUpdateDTO.EndDate,
                matchUpdateDTO.HomeTeamId, matchUpdateDTO.AwayTeamId, id);

            await EnsureTeamIsAvailableAsync(matchUpdateDTO.HomeTeamId, matchUpdateDTO.StartDate, matchUpdateDTO.EndDate, id);
            await EnsureTeamIsAvailableAsync(matchUpdateDTO.AwayTeamId, matchUpdateDTO.StartDate, matchUpdateDTO.EndDate, id);

            _mapper.Map(matchUpdateDTO, matchEntity);
            await _repo.UpdateMatchAsync(matchEntity);
        }

        public async Task PatchMatchAsync(MatchPatchDTO matchPatchedDTO, int id)
        {
            var matchEntity = await GetMatchOrThrowAsync(id);

            if(matchPatchedDTO.TournamentId.HasValue &&
                matchPatchedDTO.TournamentId.Value != matchEntity.TournamentId)
                await EnsureTournamentFKExistsOrThrowAsync(matchPatchedDTO.TournamentId.Value);
            if (matchPatchedDTO.HomeTeamId.HasValue &&
                matchPatchedDTO.HomeTeamId != matchEntity.HomeTeamId)
                await EnsureTeamFKExistsOrThrowAsync(matchPatchedDTO.HomeTeamId.Value);
            if (matchPatchedDTO.AwayTeamId.HasValue &&
                matchPatchedDTO.AwayTeamId != matchEntity.AwayTeamId)
                await EnsureTeamFKExistsOrThrowAsync(matchPatchedDTO.AwayTeamId.Value);

            _mapper.Map(matchPatchedDTO, matchEntity);

            await EnsureUniqueMatchAsync(matchEntity.StartDate, matchEntity.EndDate,
                matchEntity.HomeTeamId, matchEntity.AwayTeamId, id);

            await EnsureTeamIsAvailableAsync(
                matchEntity.HomeTeamId,
                matchEntity.StartDate,
                matchEntity.EndDate,
            id);

            await EnsureTeamIsAvailableAsync(
                matchEntity.AwayTeamId,
                matchEntity.StartDate,
                matchEntity.EndDate,
                id);


            await _repo.UpdateMatchAsync(matchEntity);

        }

        public async Task DeleteMatchAsync(int id)
        {
            var matchEntity = await GetMatchOrThrowAsync(id);
            await _repo.DeleteMatchAsync(matchEntity);
        }


        //HELPERI HELPERI HELPERI

        private async Task<Match> GetMatchOrThrowAsync(int id)
        {
            return await _repo.GetMatchAsync(id)
                ?? throw new KeyNotFoundException($"Match with id {id} not found");
        }

        private async Task EnsureTournamentFKExistsOrThrowAsync(int tournamentId)
        {
            if (!await _repo.TournamentFKExistsAsync(tournamentId))
                throw new KeyNotFoundException($"Tournament with the id {tournamentId} doesn't exist");
        }

        private async Task EnsureTeamFKExistsOrThrowAsync(int teamId)
        {
            if (!await _repo.TeamFkExistsAsync(teamId))
                throw new KeyNotFoundException($"Team with the id {teamId} doesn't exist");
        }

        private async Task EnsureUniqueMatchAsync(DateTime start, DateTime end,
            int homeTeamId, int awayTeamId, int? excludeId = null)
        {
            if (await _repo.MatchExistsAsync(start, end, homeTeamId, awayTeamId, excludeId))
                throw new InvalidOperationException
                        ($"A match named between {homeTeamId + " and " + awayTeamId} " +
                        $"has already been played on {start.Date}");
        }

        private async Task EnsureTeamIsAvailableAsync(int teamId, DateTime start, DateTime end, int? excludeMatchId = null)
        {
            if (await _repo.IsTeamBusyAsync(teamId, start, end, excludeMatchId))
                throw new InvalidOperationException($"Team with id {teamId} already has a match during this time.");
        }

    }
}
