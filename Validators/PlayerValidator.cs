namespace aplicatieHandbal.Validators;
using aplicatieHandbal.Models;
using FluentValidation;

public class PlayerValidator : AbstractValidator<Player>
{
    public PlayerValidator()
    {
        RuleFor(player => player.Name).NotEmpty().WithMessage("Name is required");
        RuleFor(player => player.Surname).NotEmpty().WithMessage("Surname is required");
       
      
    }
}
