using Domain.Dtos;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation
{
    public class UserValidator: AbstractValidator<RegisterAuthDto>
    {
        public UserValidator()
        {
            RuleFor(u => u.Name).NotEmpty().WithMessage("Name cannot be empty");
            RuleFor(u => u.Email).NotEmpty().WithMessage("Email cannot be empty");
            RuleFor(u => u.Email).EmailAddress().WithMessage("Email is not valid");
            RuleFor(u => u.Password).NotEmpty().WithMessage("Password cannot be empty");
            RuleFor(u => u.Password).Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,}$")
                .WithMessage("Password must be at least 8 characters, contain at least one uppercase letter, one lowercase letter, one number, and one special character");
        }
    }
}
