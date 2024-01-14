using Business.Repositories.UserRepository.Constants;
using Business.Repositories.UserRepository.Validation.FluentValidation;
using Business.Utilities.File;
using Core.Aspects;
using Core.Utilities.Hashing;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Repositories.UserRepository;
using Domain.Dtos;
using Domain.Entities.Concrete;

namespace Business.Repositories.UserRepository
{
    public class UserManager : IUserService
    {
        private readonly IUserDal _userDal;
        private readonly IFileService _fileService;

        public UserManager(IUserDal userDal, IFileService fileService)
        {
            _userDal = userDal;
            _fileService = fileService;
        }

        public IResult Add(RegisterAuthDto authDto)
        {
            try
            {
                HashingHelper.CreatePasswordHash(authDto.Password, out var passwordHash, out var passwordSalt);
                _fileService.SaveImageToServer(authDto.Image, out var imageUrl);

                var user = CreateUser(authDto, passwordHash, passwordSalt, imageUrl);

                _userDal.Add(user);
            }
            catch (Exception e)
            {
                return new ErrorResult(e.Message);
            }

            return new SuccessResult(UserMessages.Added);
        }

        private User CreateUser(RegisterAuthDto authDto, byte[] passwordHash, byte[] passwordSalt, string imageUrl)
        {
            return new User
            {
                Id = 0,
                Email = authDto.Email,
                Name = authDto.Name,
                ImageUrl = imageUrl,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
            };
        }

        public IDataResult<User?> GetByEmail(string email)
        {
            var user = _userDal.Get(u => u.Email == email);
            if (user == null)
            {
                return new ErrorDataResult<User?>(UserMessages.NotFound);
            }

            return new SuccessDataResult<User?>(user, UserMessages.Retrieved);
        }

        public IDataResult<List<User>> GetList()
        {
            var users = _userDal.GetAll();
            if (users == null)
            {
                return new ErrorDataResult<List<User>>(UserMessages.NotFound);
            }

            return new SuccessDataResult<List<User>>(users, UserMessages.Listed);
        }

        public IDataResult<User?> GetByUserId(int userId)
        {
            var user = _userDal.Get(u => u.Id == userId);
            if (user == null)
            {
                return new ErrorDataResult<User?>(UserMessages.NotFound);
            }

            return new SuccessDataResult<User?>(user, UserMessages.Retrieved);
        }

        [ValidationAspect(typeof(UserValidator))]
        public IResult Update(User user)
        {
            try
            {
                _userDal.Update(user);
            }
            catch (Exception e)
            {
                return new ErrorResult(e.Message);
            }

            return new SuccessResult(UserMessages.Updated);
        }

        public IResult Delete(User user)
        {
            try
            {
                _userDal.Delete(user);
            }
            catch (Exception e)
            {
                return new ErrorResult(e.Message);
            }

            return new SuccessResult(UserMessages.Deleted);
        }

        [ValidationAspect(typeof(UserChangePasswordValidator))]
        public IResult ChangePassword(UserChangePasswordDto userChangePasswordDto)
        {
            var user = _userDal.Get(u => u.Id == userChangePasswordDto.UserId);
            if (user == null)
            {
                return new ErrorResult(UserMessages.NotFound);
            }

            if (!HashingHelper.VerifyPasswordHash(userChangePasswordDto.OldPassword, user.PasswordHash, user.PasswordSalt))
            {
                return new ErrorResult(UserMessages.WrongPassword);
            }

            HashingHelper.CreatePasswordHash(userChangePasswordDto.NewPassword, out var passwordHash, out var passwordSalt);
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            try
            {
                _userDal.Update(user);
            }
            catch (Exception e)
            {
                return new ErrorResult(e.Message);
            }

            return new SuccessResult(UserMessages.PasswordChanged);
        }
    }
}
