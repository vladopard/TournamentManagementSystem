using FluentValidation;
using TournamentManagementSystem.DTOs;

namespace TournamentManagementSystem.Validation.TournamentValidation
{
    public class TournamentCreateValidator : TournamentBaseValidator<TournamentCreateDTO>
    {
        public TournamentCreateValidator() : base()
        {
            
        }

    }
}
