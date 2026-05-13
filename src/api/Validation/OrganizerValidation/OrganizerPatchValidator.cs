using FluentValidation;
using TournamentManagementSystem.DTOs.Organizer;

namespace TournamentManagementSystem.Validation.OrganizerValidation
{
    public class OrganizerPatchValidator : AbstractValidator<OrganizerPatchDTO>
    {
        public OrganizerPatchValidator() 
        {
            When(x => x.Name != null, () =>
            {
                RuleFor(x => x.Name!)
                    .NotEmpty().WithMessage("Name cannot be empty")
                    .MaximumLength(100).WithMessage("Name must be at most 100 characters");
            });

            When(x => x.ContactInfo != null, () =>
            {
                RuleFor(x => x.ContactInfo!)
                    .NotEmpty().WithMessage("Contact info cannot be empty")
                    .MaximumLength(200).WithMessage("Contact info must be at most 200 characters");
            });
        }
    }
}
