using Core.DataAccess.EntityFramework;
using DataAccess.Context.EntityFramework;
using Domain.Entities.Concrete;

namespace DataAccess.Repositories.UserRepository
{
    public class EfUserDal : EfEntityRepositoryBase<User, AppDbContext>, IUserDal
    {
    }
}
