using System.Numerics;
using FluentValidation;
using TournamentManagementSystem.DTOs;

namespace TournamentManagementSystem.Validation.TournamentValidation
{
    //You only ever need FluentValidation for your incoming models (Create/Update/Patch DTOs),
    //not for your output DTOs. 
    public abstract class TournamentBaseValidator<T> : AbstractValidator<T> 
        where T : TournamentBaseDTO
    {
        protected TournamentBaseValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required")
                .MaximumLength(100);

            RuleFor(x => x.Location)
                .NotEmpty().WithMessage("Location is required")
                .MaximumLength(200);

            RuleFor(x => x.SportType)
                .NotEmpty().WithMessage("Sport type is required")
                .MaximumLength(50);

            RuleFor(x => x.StartDate)
                .NotEqual(default(DateTime)).WithMessage("Start date is required")
                .LessThan(x => x.EndDate).WithMessage("Start date must be before end date");

            RuleFor(x => x.EndDate)
                .NotEqual(default(DateTime)).WithMessage("End date is required");

            RuleFor(x => x.OrganizerId)
                .GreaterThan(0).WithMessage("Organizer id must be a valid positive number");
        }
    }

    public class TournamentCreateValidator : TournamentBaseValidator<TournamentCreateDTO>
    {
        public TournamentCreateValidator() : base()
        {

        }

    }
    public class TournamentUpdateValidator : TournamentBaseValidator<TournamentUpdateDTO>
    {
        public TournamentUpdateValidator() : base()
        {

        }
    }

}
