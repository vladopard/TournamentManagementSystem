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
            return organizer != null ? Ok(organizer) : NotFound();
        }

        [HttpPost]
        public async Task<ActionResult<OrganizerDTO>> CreateOrganizer(
            OrganizerCreateDTO organizerCreateDTO)
        {
            try { 
                var organizerDTO = await _service.CreateOrganizerAsync(organizerCreateDTO);

                return CreatedAtRoute("GetSingleOrganizer",
                new { id = organizerDTO.OrganizerId },
                organizerDTO);
            } catch(InvalidOperationException ex)
            {
                return Conflict(ex.Message);
            }    
        }

        [HttpPut("{id}")] // Use HttpPut for full updates
        public async Task<IActionResult> UpdateOrganizer(int id, OrganizerUpdateDTO organizerUpdateDTO)
        {
            try
            {
                await _service.UpdateOrganizerAsync(organizerUpdateDTO, id);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound($"Organizer {id} not found.");
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { message = ex.Message });
            }
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

            try
            {
                await _service.PatchOrganizerAsync(organizerForPatchDTO, id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteOrganizer(int id)
        {
            try
            {
                await _service.DeleteOrganizerAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();       
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(ex.Message);
            }
        }

    }
}
