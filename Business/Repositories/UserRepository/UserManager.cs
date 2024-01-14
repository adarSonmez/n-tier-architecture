using Business.Utilities.File;
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

            return new SuccessResult();
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
                return new ErrorDataResult<User?>("User not found");
            }

            return new SuccessDataResult<User?>(user);
        }

        public IDataResult<List<User>> GetList()
        {
            var users = _userDal.GetAll();

            if (users == null)
            {
                return new ErrorDataResult<List<User>>("Users not found");
            }

            return new SuccessDataResult<List<User>>(users);
        }

        public IDataResult<User?> GetByUserId(int userId)
        {
            var user = _userDal.Get(u => u.Id == userId);

            if (user == null)
            {
                return new ErrorDataResult<User?>("User not found");
            }

            return new SuccessDataResult<User?>(user);
        }

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

            return new SuccessResult();
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

            return new SuccessResult();
        }
    }
}
