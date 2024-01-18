using Core.Entities.Concrete;
using FluentValidation;

namespace Business.Repositories.UserRepository.Validation.FluentValidation
{
    public class UserValidator: AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(u => u.Name).NotEmpty().WithMessage("Name cannot be empty");
            RuleFor(u => u.Email).NotEmpty().WithMessage("Email cannot be empty");
            RuleFor(u => u.Email).EmailAddress().WithMessage("Email is not valid");
        }
    }
}
