namespace aplicatieHandbal.Validators;
using aplicatieHandbal.Models;

using FluentValidation;

public class StaffValidator : AbstractValidator<Staff>
{
    public StaffValidator()
    {
        RuleFor(staff => staff.Name).NotEmpty().WithMessage("Name is required");
        RuleFor(staff => staff.Surname).NotEmpty().WithMessage("Surname is required");
    }
}
