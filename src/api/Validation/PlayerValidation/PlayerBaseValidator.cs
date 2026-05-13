using FluentValidation;
using TournamentManagementSystem.DTOs.Player;

namespace TournamentManagementSystem.Validation.PlayerValidation
{
    public abstract class PlayerBaseValidator<T> : AbstractValidator<T> 
        where T : PlayerBaseDTO
    {
        protected PlayerBaseValidator() 
        {
            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("First name is required")
                .MaximumLength(50).WithMessage("First name must be at most 50 characters");

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Last name is required")
                .MaximumLength(50).WithMessage("Last name must be at most 50 characters");

            RuleFor(x => x.Position)
                .NotEmpty().WithMessage("Position is required")
                .MaximumLength(50).WithMessage("Position must be at most 50 characters");

            RuleFor(x => x.DateOfBirth)
                .NotEqual(default(DateTime)).WithMessage("Date of birth is required")
                .LessThan(DateTime.UtcNow).WithMessage("Date of birth must be in the past");

            RuleFor(x => x.TeamId)
                .GreaterThan(0).WithMessage("TeamId must be a positive integer");
        }
    }
    
    public class PlayerCreateValidator : PlayerBaseValidator<PlayerCreateDTO>
    {
        public PlayerCreateValidator() : base() { }
    }

    public class PlayerUpdateValidator : PlayerBaseValidator<PlayerUpdateDTO>
    {
        public PlayerUpdateValidator() : base() { }
    }



}
