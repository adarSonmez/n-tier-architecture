using Core.DataAccess;
using Domain.Entities.Concrete;

namespace DataAccess.Repositories.UserRepository
{
    public interface IUserDal : IEntityRepository<User>
    {
    }
}
