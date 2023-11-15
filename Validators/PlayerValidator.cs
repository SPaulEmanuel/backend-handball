namespace aplicatieHandbal.Validators;
using aplicatieHandbal.Models;
using FluentValidation;

public class PlayerValidator : AbstractValidator<Player>
{
    public PlayerValidator()
    {
        RuleFor(player => player.Name).NotEmpty().WithMessage("Name is required");
        RuleFor(player => player.Surname).NotEmpty().WithMessage("Vorname is required");
        RuleFor(player => player.Age)
            .InclusiveBetween(16, 50)
            .WithMessage("Age must be between 16 and 50");

    }
}
