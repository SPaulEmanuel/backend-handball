using aplicatieHandbal.Models;
using FluentValidation;
using System.Numerics;


namespace aplicatieHandbal.Validators
{
    public class GameValidator : AbstractValidator<Game>
    {
        public GameValidator() {
            RuleFor(game => game.Title).NotEmpty().WithMessage("Title is required");
           
            RuleFor(game => game.Location).NotEmpty().WithMessage("Location is required");
  

        }
    

    }
}
