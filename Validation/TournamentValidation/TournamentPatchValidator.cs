using FluentValidation;
using TournamentManagementSystem.DTOs;


namespace TournamentManagementSystem.Validation.TournamentValidation
{
    public class TournamentPatchValidator : AbstractValidator<TournamentPatchDTO>
    {
        public TournamentPatchValidator()
        {
            RuleFor(x => x.Name)
                .MaximumLength(100)
                .When(x => x.Name != null);

            RuleFor(x => x.Location)
            .MaximumLength(200)
            .When(x => x.Location != null);

            RuleFor(x => x.SportType)
                .MaximumLength(50)
                .When(x => x.SportType != null);
                
            RuleFor(x => x.StartDate)
                .LessThan(x => x.EndDate)
                .When(x => x.StartDate.HasValue && x.EndDate.HasValue)
                .WithMessage("Start date must be before end date");

            RuleFor(x => x.EndDate)
                .GreaterThan(x => x.StartDate)
                .When(x => x.StartDate.HasValue && x.EndDate.HasValue)
                .WithMessage("End date must be after start date");

            RuleFor(x => x.OrganizerId)
                .GreaterThan(0)
                .When(x => x.OrganizerId.HasValue)
                .WithMessage("Organizer id must be a valid positive number");
        }
    }
    
}
