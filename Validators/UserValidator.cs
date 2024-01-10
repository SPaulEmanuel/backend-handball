
using FluentValidation;
using aplicatieHandbal.Models;

namespace aplicatieHandbal.Validators
{
    public class UserValidator : AbstractValidator<Users>
    {
        public UserValidator()
        {
            RuleFor(player => player.FirstName).NotEmpty().WithMessage("Name is required");
            RuleFor(player => player.LastName).NotEmpty().WithMessage("Surname is required");
        }
    }
}
