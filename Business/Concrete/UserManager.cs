using Business.Abstract;
using Core.Utilities.Hashing;
using DataAccess.Abstract;
using Domain.Dtos;
using Domain.Entities.Concrete;

namespace Business.Concrete
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
            _fileService.SaveImage(authDto.Image, out var imageUrl);

            var user = new User
            {
                Id = 0,
                Email = authDto.Email,
                Name = authDto.Name,
                ImageUrl = imageUrl,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
            };

            _userDal.Add(user);
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
