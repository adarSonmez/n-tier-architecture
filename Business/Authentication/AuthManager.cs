using Business.Repositories.UserRepository;
using Business.Repositories.UserRepository.Validation.FluentValidation;
using Core.Aspects;
using Core.Utilities.Business;
using Core.Utilities.Hashing;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using Domain.Dtos;

namespace Business.Authentication
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
            var user = _userService.GetByEmail(authDto.Email).Data;

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
            var result = BusinessRules.Run(
                CheckIfUserExists(authDto.Email),
                CheckIfImageSizeValid(authDto.Image),
                CheckIfImageIsValid(authDto.Image));

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

        private IResult CheckIfImageSizeValid(Microsoft.AspNetCore.Http.IFormFile? image)
        {
            if (image == null)
            {
                return new SuccessResult();
            }

            return image.Length > 2 * 1024 * 1024
                ? new ErrorResult("Image size must be less than 2MB")
                : new SuccessResult();
        }

        private IResult CheckIfImageIsValid(Microsoft.AspNetCore.Http.IFormFile? image)
        {
            if (image == null)
            {
                return new SuccessResult();
            }

            var validExtensions = new[] { ".jpg", ".jpeg", ".png" };
            var extension = Path.GetExtension(image.FileName).ToLower();
            return validExtensions.Contains(extension)
                ? new SuccessResult()
                : new ErrorResult("Image extension must be jpg, jpeg or png");
        }
    }
}
