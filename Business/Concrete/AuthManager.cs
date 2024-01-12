using Business.Abstract;
using Business.ValidationRules.FluentValidation;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.Hashing;
using Domain.Dtos;

namespace Business.Concrete
{
    public class AuthManager : IAuthService
    {
        private readonly IUserService _userService;

        public AuthManager(IUserService userService)
        {
            _userService = userService;
        }

        public void Login(LoginAuthDto authDto)
        {
            var user = _userService.GetByEmail(authDto.Email) ?? throw new Exception("User not found");

            if (!HashingHelper.VerifyPasswordHash(authDto.Password, user.PasswordHash, user.PasswordSalt))
            {
                throw new Exception("Wrong password");
            }

            Console.WriteLine($"Welcome {user.Name}");
        }

        public void Register(RegisterAuthDto authDto)
        {
            var validator = new UserValidator();
            ValidationTool.Validate(validator, authDto);

            _userService.Add(authDto);
        }
    }
}
