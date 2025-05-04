using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using TournamentManagementSystem.BusinessServices.BusinessInterfaces;
using TournamentManagementSystem.DTOs.Match;

namespace TournamentManagementSystem.Controllers
{
    [Route("api/matches")]
    [ApiController]
    public class MatchController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IMatchService _service;

        public MatchController(IMapper mapper, IMatchService service)
        {
            _mapper = mapper;
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MatchDTO>>> GetMatches()
        {
            return Ok(await _service.GetMatchesAsync());
        }

        [HttpGet("{id}", Name = "GetMatch")]
        public async Task<ActionResult<MatchDTO>> GetMatch(int id)
        {
            return Ok(await _service.GetMatchAsync(id));
        }

        [HttpPost]
        public async Task<ActionResult<MatchDTO>> AddMatch(MatchCreateDTO matchCreateDTO)
        {
            var matchDTO = await _service.AddMatchAsync(matchCreateDTO);
            return CreatedAtRoute("GetMatch",
                new { id = matchDTO.MatchId },
                matchDTO);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<MatchDTO>> UpdateMatch(MatchUpdateDTO matchUpdateDTO, int id)
        {
            await _service.UpdateMatchAsync(matchUpdateDTO, id);
            return NoContent();
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult<MatchDTO>> PatchMatch
            (JsonPatchDocument<MatchPatchDTO> jsonPatchDocument, int id)
        {
            var matchDTO = await _service.GetMatchAsync(id);

            var patchingDTO = _mapper.Map<MatchPatchDTO>(matchDTO);
            jsonPatchDocument.ApplyTo(patchingDTO);

            if (!ModelState.IsValid) return ValidationProblem(ModelState);
            if (!TryValidateModel(patchingDTO)) return UnprocessableEntity(ModelState);

            await _service.PatchMatchAsync(patchingDTO, id);
            return NoContent();

        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteMatch(int id)
        {
            await _service.DeleteMatchAsync(id);
            return NoContent();
        }

    }
}
