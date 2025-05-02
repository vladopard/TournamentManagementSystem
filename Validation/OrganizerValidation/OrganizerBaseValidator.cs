using FluentValidation;
using TournamentManagementSystem.DTOs.Organizer;

namespace TournamentManagementSystem.Validation.OrganizerValidation
{
    public abstract class OrganizerBaseValidator<T> : AbstractValidator<T> where T : OrganizerBaseDTO
    {
        public OrganizerBaseValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required")
                .MaximumLength(100);

            RuleFor(x => x.ContactInfo)
                .NotEmpty().WithMessage("Contact is required")
                .MaximumLength(100);
        }

    }

    public class OrganizerCreateValidator : OrganizerBaseValidator<OrganizerCreateDTO>
    {
        public OrganizerCreateValidator() : base()
        {

        }
    }

    public class OrganizerUpdateValidator : OrganizerBaseValidator<OrganizerUpdateDTO>
    {
        public OrganizerUpdateValidator() : base() { }
    }

}
