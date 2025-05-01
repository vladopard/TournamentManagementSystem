using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using TournamentManagementSystem.BusinessServices;
using TournamentManagementSystem.DTOs.Organizer;

namespace TournamentManagementSystem.Controllers
{
    [Route("api/organizers")]
    [ApiController]
    public class OrganizerController : ControllerBase
    {
        private readonly IOrganizerService _service;
        private readonly IMapper _mapper;

        public OrganizerController(IOrganizerService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrganizerDTO>>>GetOrganizers()
        {
            var organizers = await _service.GetAllOrganizersAsync();
            return Ok(organizers);
        }

        [HttpGet("{id}", Name ="GetSingleOrganizer")]
        public async Task<ActionResult<OrganizerDTO>> GetOrganizer(int id)
        {
            var organizer = await _service.GetOrganizerAsync(id);
            return Ok(organizer);
        }

        [HttpPost]
        public async Task<ActionResult<OrganizerDTO>> CreateOrganizer(
            OrganizerCreateDTO organizerCreateDTO)
        {
                var organizerDTO = await _service.CreateOrganizerAsync(organizerCreateDTO);
                return CreatedAtRoute("GetSingleOrganizer",
                new { id = organizerDTO.OrganizerId },
                organizerDTO);
             
        }

        [HttpPut("{id}")] // Use HttpPut for full updates
        public async Task<IActionResult> UpdateOrganizer(int id, OrganizerUpdateDTO organizerUpdateDTO)
        {

            await _service.UpdateOrganizerAsync(organizerUpdateDTO, id);
            return NoContent();
            
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchOrganizer(
            int id, [FromBody] JsonPatchDocument<OrganizerPatchDTO> patchDocument)
        {
            if (patchDocument == null) return BadRequest();

            var organizerDTO = await _service.GetOrganizerAsync(id);
            if (organizerDTO == null) return NotFound($"Organizer {id} not found.");

            var organizerForPatchDTO = _mapper.Map<OrganizerPatchDTO>(organizerDTO);
            patchDocument.ApplyTo(organizerForPatchDTO, ModelState);

            if (!ModelState.IsValid) return ValidationProblem(ModelState);
            if (!TryValidateModel(organizerForPatchDTO)) return UnprocessableEntity(ModelState);

            await _service.PatchOrganizerAsync(organizerForPatchDTO, id);
            return NoContent();

        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteOrganizer(int id)
        {

            await _service.DeleteOrganizerAsync(id);
            return NoContent();

        }

    }
}
