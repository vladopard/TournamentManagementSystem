using FluentValidation;
using TournamentManagementSystem.DTOs.Match;

namespace TournamentManagementSystem.Validation.MatchValidation
{
    public class MatchPatchValidator : AbstractValidator<MatchPatchDTO>
    {
        public MatchPatchValidator()
        {
            When(x => x.StartDate.HasValue || x.EndDate.HasValue, () =>
            {
                RuleFor(x => x.StartDate!.Value)
                    .NotEqual(default(DateTime)).WithMessage("Start date is required")
                    .LessThan(x => x.EndDate!.Value).WithMessage("Start date must be before end date");

                RuleFor(x => x.EndDate!.Value)
                    .NotEqual(default(DateTime)).WithMessage("End date is required")
                    .GreaterThan(x => x.StartDate!.Value).WithMessage("End date must be after start date");
            });

            When(x => x.ScoreHome.HasValue, () =>
            {
                RuleFor(x => x.ScoreHome!.Value)
                    .GreaterThanOrEqualTo(0).WithMessage("Home score must be zero or greater");
            });

            When(x => x.ScoreAway.HasValue, () =>
            {
                RuleFor(x => x.ScoreAway!.Value)
                    .GreaterThanOrEqualTo(0).WithMessage("Away score must be zero or greater");
            });

            When(x => x.TournamentId.HasValue, () =>
            {
                RuleFor(x => x.TournamentId!.Value)
                    .GreaterThan(0).WithMessage("TournamentId must be a positive integer");
            });

            When(x => x.HomeTeamId.HasValue, () =>
            {
                RuleFor(x => x.HomeTeamId!.Value)
                    .GreaterThan(0).WithMessage("HomeTeamId must be a positive integer");
            });

            When(x => x.AwayTeamId.HasValue, () =>
            {
                RuleFor(x => x.AwayTeamId!.Value)
                    .GreaterThan(0).WithMessage("AwayTeamId must be a positive integer");
            });

            When(x => x.HomeTeamId.HasValue && x.AwayTeamId.HasValue, () =>
            {
                RuleFor(x => x.HomeTeamId!.Value)
                    .NotEqual(x => x.AwayTeamId!.Value)
                    .WithMessage("Home and away teams must be different");
            });
        }
    }
}
