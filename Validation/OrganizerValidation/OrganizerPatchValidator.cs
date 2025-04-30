using FluentValidation;
using TournamentManagementSystem.DTOs.Organizer;

namespace TournamentManagementSystem.Validation.OrganizerValidation
{
    public class OrganizerPatchValidator : AbstractValidator<OrganizerPatchDTO>
    {
        public OrganizerPatchValidator() 
        {
            RuleFor(x => x.Name)
               .MaximumLength(100)
               .When(x => x.Name != null);

            RuleFor(x => x.ContactInfo)
                .MaximumLength(100)
                .When(x => x.ContactInfo != null);
        }
    }
}
