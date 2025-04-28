using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using TournamentManagementSystem.BusinessServices.BusinessInterfaces;
using TournamentManagementSystem.DTOs;
using TournamentManagementSystem.DTOs.Tournament;

namespace TournamentManagementSystem.Controllers
{
    [Route("api/tournaments")]
    [ApiController]
    public class TournamentController : ControllerBase
    {
        private readonly ITournamentService _service;
        private readonly IMapper _mapper;

        public TournamentController(ITournamentService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TournamentDTO>>> GetTournaments()
        {
            var tournaments = await _service.GetAllTournamentsAsync();
            return Ok(tournaments);
        }

        [HttpGet("{id}", Name = "GetSingleTournament")]
        public async Task<ActionResult<TournamentDTO>> GetSingleTournament(int id)
        {
            var tournament = await _service.GetSingleTournamentAsync(id);
            return tournament == null ? NotFound("No tournament found") : Ok(tournament);
        }

        [HttpPost]
        public async Task<ActionResult<TournamentDTO>> CreateNewTournament(
            TournamentCreateDTO tournamentCreateDTO)
        {
            var createdTournamentDTO = 
                await _service.CreateTournamentAsync(tournamentCreateDTO);

            if (createdTournamentDTO == null)
            {
                return BadRequest("Invalid data or OrganizerId does not exist.");
            }

            return CreatedAtAction("GetSingleTournament",
                new { id = createdTournamentDTO.TournamentId },
                createdTournamentDTO);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateTournament(
            TournamentUpdateDTO tournamentUpdateDTO, int id)
        {
            var result = await _service.UpdateTournamentAsync(tournamentUpdateDTO, id);
            return result ? NoContent() : NotFound("Tournament not found/or invalid organizer");
            //FIX separate message for not found
            //UPSERTING NOT ALLOWED YET
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult> PatchTournament(
            JsonPatchDocument<TournamentPatchDTO> patchDocument, int id)
        {
            if (patchDocument == null) return BadRequest();

            var tournamentDTO = await _service.GetSingleTournamentAsync(id);
            if (tournamentDTO == null) return NotFound();

            var tournamentForPatchDTO = _mapper.Map<TournamentPatchDTO>(tournamentDTO);
            //isto fali negde provera za organizera ako patchujemo bas to polje

            patchDocument.ApplyTo(tournamentForPatchDTO, ModelState);
            if (!ModelState.IsValid) return ValidationProblem(ModelState);

            var success = await _service.PatchTournamentAsync(tournamentForPatchDTO, id);
            return success ? NoContent() : NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteTournament(int id)
        {
            var success = await _service.DeleteTournamentAsync(id);
            return success ? NoContent() : NotFound();
        }
    }
}
