using aplicatieHandbal.Models;
using FluentValidation;
using System.Numerics;


namespace aplicatieHandbal.Validators
{
    public class GameValidator : AbstractValidator<Game>
    {
        public GameValidator() {
            RuleFor(game => game.Title).NotEmpty().WithMessage("Title is required");
            RuleFor(game => game.Date)
               .NotEmpty().WithMessage("Date is required")
               .Must(BeAValidDate).WithMessage("Invalid date format");
            RuleFor(game => game.Location).NotEmpty().WithMessage("Location is required");
            RuleFor(game => game.Result)
                .IsInEnum().WithMessage("Invalid game result");
            RuleFor(game => game.Status)
                .IsInEnum().WithMessage("Invalid game status");

        }
        private bool BeAValidDate(DateTime date)
        {
            return date >= DateTime.Now;
        }

    }
}
