
using FluentValidation;
using aplicatieHandbal.Models;

namespace aplicatieHandbal.Validators
{
    public class UserValidator : AbstractValidator<Users>
    {
        public UserValidator()
        {
            RuleFor(user => user.FirstName).NotEmpty().WithMessage("FirstName is required");
            RuleFor(user => user.LastName).NotEmpty().WithMessage("LastName is required");
            RuleFor(user => user.Username).NotEmpty().WithMessage("Username is required");
            RuleFor(user => user.Password).NotEmpty().WithMessage("Password is required");
            RuleFor(user => user.UserType).NotEmpty().WithMessage("UserType is required");
            RuleFor(user => user.ImageUrl).NotEmpty().WithMessage("ImageUrl is required");
        }
    }
}
