using FluentValidation;
using TournamentManagementSystem.DTOs.Player;

namespace TournamentManagementSystem.Validation.PlayerValidation
{
    public class PlayerPatchValidator : AbstractValidator<PlayerPatchDTO>
    {
        public PlayerPatchValidator() 
        {
            When(x => x.FirstName != null, () =>
            {
                RuleFor(x => x.FirstName!)
                    .NotEmpty().WithMessage("First name cannot be empty")
                    .MaximumLength(50).WithMessage("First name must be at most 50 characters");
            });

            When(x => x.LastName != null, () =>
            {
                RuleFor(x => x.LastName!)
                    .NotEmpty().WithMessage("Last name cannot be empty")
                    .MaximumLength(50).WithMessage("Last name must be at most 50 characters");
            });

            When(x => x.Position != null, () =>
            {
                RuleFor(x => x.Position!)
                    .NotEmpty().WithMessage("Position cannot be empty")
                    .MaximumLength(50).WithMessage("Position must be at most 50 characters");
            });

            When(x => x.DateOfBirth.HasValue, () =>
            {
                RuleFor(x => x.DateOfBirth!.Value)
                    .NotEqual(default(DateTime)).WithMessage("Date of birth is required")
                    .LessThan(DateTime.UtcNow).WithMessage("Date of birth must be in the past");
            });

            When(x => x.TeamId.HasValue, () =>
            {
                RuleFor(x => x.TeamId!.Value)
                    .GreaterThan(0).WithMessage("TeamId must be a positive integer");
            });

        }
    }
}
