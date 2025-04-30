using AutoMapper;
using TournamentManagementSystem.DTOs;
using TournamentManagementSystem.DTOs.Organizer;
using TournamentManagementSystem.Entities;
using TournamentManagementSystem.Repositories;

namespace TournamentManagementSystem.BusinessServices
{
    public class OrganizerService : IOrganizerService
    {
        private readonly ISystemRepository _repo;
        private readonly IMapper _mapper;

        public OrganizerService(ISystemRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<IEnumerable<OrganizerDTO>> GetAllOrganizersAsync()
        {
            var organizers = await _repo.GetAllOrganizersAsync();
            return _mapper.Map<IEnumerable<OrganizerDTO>>(organizers);
        }

        public async Task<OrganizerDTO?> GetOrganizerAsync(int id)
        {
            var organizer = await _repo.GetOrganizerAsync(id);
            if (organizer == null) return null;
            return _mapper.Map<OrganizerDTO>(organizer);
        }
        public async Task<OrganizerDTO> CreateOrganizerAsync(OrganizerCreateDTO organizerCreateDTO)
        {
            await EnsureUniqueAsync(organizerCreateDTO.Name, "", organizerCreateDTO.ContactInfo, "");

            var organizerEntity = _mapper.Map<Organizer>(organizerCreateDTO);
            await _repo.AddOrganizerAsync(organizerEntity);

            // 4) Return DTO (ID is set)
            return _mapper.Map<OrganizerDTO>(organizerEntity);
        }

        

        public async Task UpdateOrganizerAsync(OrganizerUpdateDTO organizerUpdateDTO, int id)
        {
            var organizerEntity = await GetOrganizerOrThrow(id);

            await EnsureUniqueAsync(organizerUpdateDTO.Name, organizerEntity.Name,
                organizerUpdateDTO.ContactInfo, organizerEntity.ContactInfo, id);

            _mapper.Map(organizerUpdateDTO, organizerEntity);
            await _repo.UpdateOrganizerAsync(organizerEntity);
        }

        public async Task PatchOrganizerAsync(OrganizerPatchDTO organizerPatched, int id)
        {
            var organizerEntity = await GetOrganizerOrThrow(id);

            await EnsureUniqueAsync(organizerPatched.Name!, organizerEntity.Name,
                organizerPatched.ContactInfo!, organizerEntity.ContactInfo, id);

            _mapper.Map(organizerPatched, organizerEntity);
            await _repo.UpdateOrganizerAsync(organizerEntity);
        }

        public async Task DeleteOrganizerAsync(int id)
        {
            var organizer = await GetOrganizerOrThrow(id);

            // 2) Business rule: no tournaments allowed
            if (await _repo.OrganizerHasTournamentsAsync(id))
                throw new InvalidOperationException("Cannot delete organizer because they have associated tournaments.");

            // 3) Delete
            await _repo.DeleteOrganizerAsync(organizer);
        }

        //SUMNJIVE METODE KOJE RESAVAJU DRY
        private async Task<Organizer> GetOrganizerOrThrow(int id)
        {
            return await _repo.GetOrganizerAsync(id)
                ?? throw new KeyNotFoundException($"Organizer {id} not found.");
        }

        private async Task EnsureUniqueAsync(string newName, string currentName,
            string newContact, string currentContact, int? excludedId = null)
        {
            var errors = new List<string>();



            if (newName != currentName
                && await _repo.OrganizerNameExistsAsync(newName, excludedId))
            {
                errors.Add($"An organizer named '{newName}' already exists.");
            }
            if (newContact != currentContact
            && await _repo.OrganizerContactInfoExistsAsync(newContact, excludedId))
            {
                errors.Add($"An organizer with contact '{newContact}' already exists.");
            }

            if (errors.Any())
            {
                throw new InvalidOperationException(string.Join(" ", errors));
            }
        }
    }
}
