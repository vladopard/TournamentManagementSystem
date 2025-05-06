using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TournamentManagementSystem.BusinessServices.BusinessInterfaces;
using TournamentManagementSystem.DTOs.PlayerStats;

namespace TournamentManagementSystem.Controllers
{
    [Route("api/players/{playerId}/stats")]
    [ApiController]
    public class PlayerStatsController : ControllerBase
    {
        private readonly IPlayerMatchStatsService _service;

        public PlayerStatsController(IPlayerMatchStatsService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PlayerMatchStatsDTO>>> 
            GetAllStatsForPlayer(int playerId)
        {
            var statsForPlayerDTO = await _service.GetAllStatsForPlayerAsync(playerId);
            return Ok(statsForPlayerDTO);
        }

        [HttpGet("{matchId}")]
        public async Task<ActionResult<PlayerMatchStatsDTO>> 
            GetStatsForPlayerFromOneMatch(int playerId, int matchId)
        {
            var statsDTO = await _service.GetStatsForPlayerFromOneMatchAsync(playerId, matchId);
            return Ok(statsDTO);
        }
    }
}
