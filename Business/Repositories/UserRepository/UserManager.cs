using Business.Utilities.File;
using Core.Utilities.Hashing;
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

        public void Add(RegisterAuthDto authDto)
        {
            HashingHelper.CreatePasswordHash(authDto.Password, out var passwordHash, out var passwordSalt);
            _fileService.SaveImageToServer(authDto.Image, out var imageUrl);

            var user = CreateUser(authDto, passwordHash, passwordSalt, imageUrl);

            _userDal.Add(user);
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

        public User? GetByEmail(string email)
        {
            return _userDal.Get(u => u.Email == email);
        }

        public List<User> GetList()
        {
            return _userDal.GetAll();
        }
    }
}
