using Business.Abstract;
using Business.ValidationRules.FluentValidation;
using Core.Aspects;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.Business;
using Core.Utilities.Hashing;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
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

        public IResult Login(LoginAuthDto authDto)
        {
            var user = _userService.GetByEmail(authDto.Email);

            if (user == null)
            {
                return new ErrorResult("User not found");
            }

            if (!HashingHelper.VerifyPasswordHash(authDto.Password, user.PasswordHash, user.PasswordSalt))
            {
                return new ErrorResult("Wrong password");
            }

            return new SuccessResult("Login successful");
        }

        [ValidationAspect(typeof(UserValidator))]
        public IResult Register(RegisterAuthDto authDto)
        {
            var result = BusinessRules.Run(CheckIfUserExists(authDto.Email));

            if (result.Success)
            {
                result.Message = "User registered";
                _userService.Add(authDto);
            }

            return result;
        }

        private IResult CheckIfUserExists(string email)
        {
            return _userService.GetByEmail(email) != null
                ? new ErrorResult("User already exists")
                : new SuccessResult();
        }
    }
}
