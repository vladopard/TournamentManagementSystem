using FluentValidation;
using TournamentManagementSystem.DTOs.Team;

namespace TournamentManagementSystem.Validation.TeamValidation
{
    public class TeamPatchValidator : AbstractValidator<TeamPatchDTO>
    {
        public TeamPatchValidator()
        {
            RuleFor(x => x.Name)
               .MaximumLength(100)
               .When(x => x.Name != null);

            RuleFor(x => x.Coach)
                .MaximumLength(100)
                .When(x => x.Coach != null);

            RuleFor(x => x.TournamentId)
                .GreaterThan(0)
                .When(x => x.TournamentId.HasValue)
                .WithMessage("Tournament id must be a valid positive number");
        }
    }
}
