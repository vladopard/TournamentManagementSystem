using FluentValidation;
using TournamentManagementSystem.DTOs.Team;

namespace TournamentManagementSystem.Validation.TeamValidation
{
    public class TeamPatchValidator : AbstractValidator<TeamPatchDTO>
    {
        public TeamPatchValidator()
        {
            When(x => x.Name != null, () =>
            {
                RuleFor(x => x.Name!)
                    .NotEmpty().WithMessage("Name cannot be empty")
                    .MaximumLength(100).WithMessage("Name must be at most 100 characters");
            });

            When(x => x.Coach != null, () =>
            {
                RuleFor(x => x.Coach!)
                    .NotEmpty().WithMessage("Coach cannot be empty")
                    .MaximumLength(100).WithMessage("Coach must be at most 100 characters");
            });

            When(x => x.TournamentId.HasValue, () =>
            {
                RuleFor(x => x.TournamentId!.Value)
                    .GreaterThan(0)
                    .WithMessage("Tournament id must be a valid positive number");
            });
        }
    }
}
