using AutoMapper;
using Microsoft.AspNetCore.Http;
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

        [HttpGet("{id}", Name ="GetTeam")]
        public async Task<ActionResult<TeamDTO>> GetTeam(int id)
        {
            var team = await _service.GetTeamAsync(id);
            return Ok(team);
        }
    }
}
