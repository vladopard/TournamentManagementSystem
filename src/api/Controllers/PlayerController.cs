using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TournamentManagementSystem.BusinessServices.BusinessInterfaces;
using TournamentManagementSystem.DTOs.Parameters;
using TournamentManagementSystem.DTOs.Player;
using TournamentManagementSystem.Helpers;

namespace TournamentManagementSystem.Controllers
{
    [Route("api/players")]
    [ApiController]
    public class PlayerController : ControllerBase
    {
        private readonly IPlayerService _service;
        private readonly IMapper _mapper;
        public PlayerController(IPlayerService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }


        [HttpGet]
        public async Task<ActionResult<PagedList<PlayerDTO>>> GetAllPlayers(
            [FromQuery] PlayerParameters playerParameters)
        {
            var pagedPlayers = await _service.GetAllPlayersPagedAsync(playerParameters);
            Response.Headers.Append("X-Pagination", JsonConvert.SerializeObject(pagedPlayers.MetaData));
            return Ok(pagedPlayers);
        }

        //pre paginacije
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<PlayerDTO>>> GetAllPlayers()
        //    => Ok(await _service.GetAllPlayersAsync());

        [HttpGet("{id}", Name = "GetPlayer")]
        public async Task<ActionResult<PlayerDTO>> GetPlayer(int id)
            => Ok(await _service.GetPlayerAsync(id));
        
        [HttpPost]
        public async Task<ActionResult<PlayerDTO>> AddPlayer(PlayerCreateDTO playerCreateDTO)
        {
            var playerDTO = await _service.AddPlayerAsync(playerCreateDTO);
            return CreatedAtRoute("GetPlayer",
                new { id = playerDTO.PlayerId },
                playerDTO);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdatePlayer(PlayerUpdateDTO playerUpdateDTO, int id)
        {
            await _service.UpdatePlayerAsync(playerUpdateDTO, id);
            return NoContent();
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult> PatchPlayer(
            JsonPatchDocument<PlayerPatchDTO> jsonPatchDocument, int id)
        {
            var playerDTO = await _service.GetPlayerAsync(id);

            var playerForPatchingDTO = _mapper.Map<PlayerPatchDTO>(playerDTO);
            jsonPatchDocument.ApplyTo(playerForPatchingDTO);

            if (!ModelState.IsValid) return ValidationProblem(ModelState);
            if (!TryValidateModel(playerForPatchingDTO)) return UnprocessableEntity(ModelState);

            await _service.PatchPlayerAsync(playerForPatchingDTO, id);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletePlayer(int id)
        {
            await _service.DeletePlayerAsync(id);
            return NoContent();
        }
    }
}
