using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using TournamentManagementSystem.BusinessServices;
using TournamentManagementSystem.DTOs.Team;

namespace TournamentManagementSystem.Controllers
{
    [Route("api/teams")]
    [ApiController]
    public class TeamController : ControllerBase
    {
        private readonly ITeamService _service;
        private readonly IMapper _mapper;

        public TeamController(ITeamService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TeamDTO>>> GetTeams()
        {
            return Ok(await _service.GetAllTeamsAsync());
        }

        [HttpGet("{id}", Name = "GetTeam")]
        public async Task<ActionResult<TeamDTO>> GetTeam(int id)
        {
            var team = await _service.GetTeamAsync(id);
            return Ok(team);
        }

        [HttpPost]
        public async Task<ActionResult<TeamDTO>> AddTeam(TeamCreateDTO teamCreateDTO)
        {
            var teamDTO = await _service.AddTeamAsync(teamCreateDTO);
            return CreatedAtRoute("GetTeam",
                new { id = teamDTO.TournamentId },
                teamDTO);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateTeam(TeamUpdateDTO teamUpdateDTO, int id)
        {
            await _service.UpdateTeamAsync(teamUpdateDTO, id);
            return NoContent();
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult> PatchTeam(
            JsonPatchDocument<TeamPatchDTO> patchDocument, int id)
        {
            var teamDTO = await _service.GetTeamAsync(id);

            var teamForPatchingDTO = _mapper.Map<TeamPatchDTO>(teamDTO);
            patchDocument.ApplyTo(teamForPatchingDTO);

            if (!ModelState.IsValid) return ValidationProblem(ModelState);
            if (!TryValidateModel(teamForPatchingDTO)) return UnprocessableEntity(ModelState);

            await _service.PatchTeamAsync(teamForPatchingDTO, id);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteTeam(int id)
        {
            await _service.DeleteTeamAsync(id);
            return NoContent();
        }

    }
}
