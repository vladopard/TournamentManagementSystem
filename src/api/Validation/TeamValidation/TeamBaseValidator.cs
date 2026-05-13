using FluentValidation;
using TournamentManagementSystem.DTOs.Team;

namespace TournamentManagementSystem.Validation.TeamValidation
{
    public abstract class TeamBaseValidator<T> : AbstractValidator<T> 
        where T : TeamBaseDTO
    {
        protected TeamBaseValidator() 
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required")
                .MaximumLength(100);

            RuleFor(x => x.Coach)
                .NotEmpty().WithMessage("Location is required")
                .MaximumLength(200);

            RuleFor(x => x.TournamentId)
                .GreaterThan(0).WithMessage("Tournament id must be a valid positive number");
        }
    }

    public class TeamCreateValidator : TeamBaseValidator<TeamCreateDTO>
    {
        public TeamCreateValidator() : base() { }
    }

    public class TeamUpdateValidator : TeamBaseValidator<TeamUpdateDTO>
    {
        public TeamUpdateValidator() : base () { }
    }


}
