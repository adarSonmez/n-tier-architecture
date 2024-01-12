using Domain.Dtos;
using Domain.Entities.Concrete;

namespace Business.Abstract
{
    public interface IUserService
    {
        void Add(AuthDto authDto);
        List<User> GetList();
    }
}
