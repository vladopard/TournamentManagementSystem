using FluentValidation;
using TournamentManagementSystem.DTOs;

namespace TournamentManagementSystem.Validation.TournamentValidation
{
    public class TournamentUpdateValidator : TournamentBaseValidator<TournamentUpdateDTO>
    {
        public TournamentUpdateValidator() : base()
        {
            
        }
    }
}
