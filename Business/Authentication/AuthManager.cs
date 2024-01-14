using Business.Authentication.Constants;
using Business.Authentication.Validation.FluentValidation;
using Business.Repositories.UserRepository;
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
                return new ErrorResult(Messages.UserNotFound);
            }

            if (!HashingHelper.VerifyPasswordHash(authDto.Password, user.PasswordHash, user.PasswordSalt))
            {
                return new ErrorResult(Messages.UserPasswordWrong);
            }

            return new SuccessResult(Messages.UserLoginSuccessful);
        }

        [ValidationAspect(typeof(AuthValidator))]
        public IResult Register(RegisterAuthDto authDto)
        {
            var result = BusinessRules.Run(
                CheckIfUserExists(authDto.Email),
                CheckIfImageSizeValid(authDto.Image),
                CheckIfImageIsValid(authDto.Image));

            if (result.Success)
            {
                result.Message = Messages.UserRegistered;
                _userService.Add(authDto);
            }

            return result;
        }

        private IResult CheckIfUserExists(string email)
        {
            return _userService.GetByEmail(email) != null
                ? new ErrorResult(Messages.UserAlreadyExists)
                : new SuccessResult();
        }

        private IResult CheckIfImageSizeValid(Microsoft.AspNetCore.Http.IFormFile? image)
        {
            if (image == null)
            {
                return new SuccessResult();
            }

            return image.Length > 2 * 1024 * 1024
                ? new ErrorResult(Messages.UserImageSizeInvalid)
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
                : new ErrorResult(Messages.UserImageExtensionIsValid);
        }
    }
}
