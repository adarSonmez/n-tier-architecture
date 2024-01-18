using Business.Authentication.Constants;
using Business.Authentication.Validation.FluentValidation;
using Business.Repositories.UserRepository;
using Core.Aspects;
using Core.Entities.Concrete;
using Core.Utilities.Business;
using Core.Utilities.Hashing;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using Core.Utilities.Security.Jwt;
using Domain.Dtos;

namespace Business.Authentication
{
    public class AuthManager : IAuthService
    {
        private readonly IUserService _userService;
        private readonly ITokenHandler _tokenHandler;

        public AuthManager(IUserService userService, ITokenHandler tokenHandler)
        {
            _userService = userService;
            _tokenHandler = tokenHandler;
        }

        public IDataResult<Token?> Login(LoginAuthDto authDto)
        {
            var user = _userService.GetByEmail(authDto.Email).Data;

            if (user == null)
            {
                return new ErrorDataResult<Token?>(AuthMessages.UserNotFound);
            }

            if (!HashingHelper.VerifyPasswordHash(authDto.Password, user.PasswordHash, user.PasswordSalt))
            {
                return new ErrorDataResult<Token?>(AuthMessages.UserPasswordWrong);
            }

            List<OperationClaim>? operationClaims = _userService.GetClaims(user.Id).Data;
            if (operationClaims == null)
            {
                return new ErrorDataResult<Token?>(AuthMessages.UserClaimsNotFound);
            }

            var token = _tokenHandler.CreateAccessToken(user, operationClaims);

            return new SuccessDataResult<Token?>(token, AuthMessages.UserLoginSuccessful);
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
                result.Message = AuthMessages.UserRegistered;
                _userService.Add(authDto);
            }

            return result;
        }

        private IResult CheckIfUserExists(string email)
        {
            return _userService.GetByEmail(email).Data != null
                ? new ErrorResult(AuthMessages.UserAlreadyExists)
                : new SuccessResult();
        }

        private IResult CheckIfImageSizeValid(Microsoft.AspNetCore.Http.IFormFile? image)
        {
            if (image == null)
            {
                return new SuccessResult();
            }

            return image.Length > 2 * 1024 * 1024
                ? new ErrorResult(AuthMessages.UserImageSizeInvalid)
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
                : new ErrorResult(AuthMessages.UserImageExtensionInvalid);
        }
    }
}
