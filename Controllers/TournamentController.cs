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
            var tournamentDTO = await _service.GetSingleTournamentAsync(id);
            return tournamentDTO == null ? NotFound("No tournament found") : Ok(tournamentDTO);
        }

        [HttpPost]
        public async Task<ActionResult<TournamentDTO>> CreateNewTournament(
            TournamentCreateDTO tournamentCreateDTO)
        {
            try
            {
                var createdTournamentDTO =
                await _service.CreateTournamentAsync(tournamentCreateDTO);
                
                return CreatedAtRoute("GetSingleTournament",
                new { id = createdTournamentDTO.TournamentId },
                createdTournamentDTO);
            }
            catch (KeyNotFoundException ex)
            {
                // FK rule failed
                return NotFound(new { message = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                // Unique-index rule failed
                return Conflict(new { message = ex.Message });
            }

        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateTournament(
            TournamentUpdateDTO tournamentUpdateDTO, int id)
        {
            try 
            {
                await _service.UpdateTournamentAsync(tournamentUpdateDTO, id);
                return NoContent();
                //FIX separate message for not found
                //UPSERTING NOT ALLOWED YET
            }
            catch (KeyNotFoundException ex)
            {
                // either tournament or organizer missing
                return NotFound(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                // uniqueness violation
                return Conflict(ex.Message);
            }

        }

        [HttpPatch("{id}")]
        public async Task<ActionResult> PatchTournament(
            JsonPatchDocument<TournamentPatchDTO> patchDocument, int id)
        {
            if (patchDocument == null) return BadRequest();

            var tournamentDTO = await _service.GetSingleTournamentAsync(id);
            if (tournamentDTO == null) return NotFound($"Tournament {id} not found.");

            var tournamentForPatchDTO = _mapper.Map<TournamentPatchDTO>(tournamentDTO);
            patchDocument.ApplyTo(tournamentForPatchDTO, ModelState);

            if (!ModelState.IsValid) return ValidationProblem(ModelState);
            if(!TryValidateModel(tournamentForPatchDTO)) return UnprocessableEntity(ModelState);

            try
            {
                await _service.PatchTournamentAsync(tournamentForPatchDTO, id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(ex.Message);
            }

        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteTournament(int id)
        {
            try
            {
                await _service.DeleteTournamentAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound($"Tournament {id} not found.");
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(ex.Message);
            }

        }
    }
}
