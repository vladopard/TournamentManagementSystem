using FluentValidation;
using TournamentManagementSystem.DTOs;


namespace TournamentManagementSystem.Validation.TournamentValidation
{
    public class TournamentPatchValidator : AbstractValidator<TournamentPatchDTO>
    {
        public TournamentPatchValidator()
        {
            When(x => x.Name != null, () =>
            {
                RuleFor(x => x.Name!)
                    .NotEmpty().WithMessage("Name cannot be empty")
                    .MaximumLength(100).WithMessage("Name must be at most 100 characters");
            });

            When(x => x.Location != null, () =>
            {
                RuleFor(x => x.Location!)
                    .NotEmpty().WithMessage("Location cannot be empty")
                    .MaximumLength(200).WithMessage("Location must be at most 200 characters");
            });

            When(x => x.SportType != null, () =>
            {
                RuleFor(x => x.SportType!)
                    .NotEmpty().WithMessage("Sport type cannot be empty")
                    .MaximumLength(50).WithMessage("Sport type must be at most 50 characters");
            });

            When(x => x.StartDate.HasValue && x.EndDate.HasValue, () =>
            {
                RuleFor(x => x.StartDate!.Value)
                    .NotEqual(default(DateTime)).WithMessage("Start date is required")
                    .LessThan(x => x.EndDate!.Value).WithMessage("Start date must be before end date");

                RuleFor(x => x.EndDate!.Value)
                    .NotEqual(default(DateTime)).WithMessage("End date is required")
                    .GreaterThan(x => x.StartDate!.Value).WithMessage("End date must be after start date");
            });

            When(x => x.OrganizerId.HasValue, () =>
            {
                RuleFor(x => x.OrganizerId!.Value)
                    .GreaterThan(0).WithMessage("Organizer id must be a valid positive number");
            });
        }
    }
    
}
