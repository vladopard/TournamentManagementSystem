using FluentValidation;
using TournamentManagementSystem.DTOs.Match;

namespace TournamentManagementSystem.Validation.MatchValidation
{
    public class MatchBaseValidator<T> : AbstractValidator<T> 
        where T : MatchBaseDTO
    {
        public MatchBaseValidator() 
        {
            RuleFor(x => x.StartDate)
                .NotEqual(default(DateTime)).WithMessage("Start date is required")
                .LessThan(x => x.EndDate).WithMessage("Start date must be before end date");

            RuleFor(x => x.EndDate)
                .NotEqual(default(DateTime)).WithMessage("End date is required");

            // Scores
            RuleFor(x => x.ScoreHome)
                .GreaterThanOrEqualTo(0).WithMessage("Home score must be zero or greater");
            RuleFor(x => x.ScoreAway)
                .GreaterThanOrEqualTo(0).WithMessage("Away score must be zero or greater");

            // FKs
            RuleFor(x => x.TournamentId)
                .GreaterThan(0).WithMessage("TournamentId must be a positive integer");
            RuleFor(x => x.HomeTeamId)
                .GreaterThan(0).WithMessage("HomeTeamId must be a positive integer");
            RuleFor(x => x.AwayTeamId)
                .GreaterThan(0).WithMessage("AwayTeamId must be a positive integer");

            // Business rule: teams must differ
            RuleFor(x => x.HomeTeamId)
                .NotEqual(x => x.AwayTeamId)
                .WithMessage("Home and away teams must be different");
        }

    }

    public class MatchCreateValidator : MatchBaseValidator<MatchCreateDTO>
    {
        public MatchCreateValidator() : base() { }
    }

    // 3) Update validator
    public class MatchUpdateValidator : MatchBaseValidator<MatchUpdateDTO>
    {
        public MatchUpdateValidator() : base() { }
    }
}
