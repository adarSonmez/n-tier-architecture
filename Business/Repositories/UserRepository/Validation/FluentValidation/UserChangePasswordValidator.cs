using Domain.Dtos;
using FluentValidation;

namespace Business.Repositories.UserRepository.Validation.FluentValidation
{
    public class UserChangePasswordValidator : AbstractValidator<UserChangePasswordDto>
    {
        public UserChangePasswordValidator()
        {
            RuleFor(u => u.UserId).NotEmpty().WithMessage("UserId cannot be empty");
            RuleFor(u => u.OldPassword).NotEmpty().WithMessage("OldPassword cannot be empty");
            RuleFor(u => u.NewPassword).NotEmpty().WithMessage("NewPassword cannot be empty");
            RuleFor(u => u.NewPassword).Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,}$")
                .WithMessage("NewPassword must be at least 8 characters, contain at least one uppercase letter, one lowercase letter, one number, and one special character");
        }
    }
}
