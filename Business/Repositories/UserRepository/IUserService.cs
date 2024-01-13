using Domain.Dtos;
using Domain.Entities.Concrete;

namespace Business.Repositories.UserRepository
{
    public interface IUserService
    {
        void Add(RegisterAuthDto authDto);
        List<User> GetList();
        User? GetByEmail(string email);
    }
}
