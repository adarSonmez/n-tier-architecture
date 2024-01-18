using Core.DataAccess;
using Core.Entities.Concrete;

namespace DataAccess.Repositories.UserRepository
{
    public interface IUserDal : IEntityRepository<User>
    {
        List<OperationClaim> GetClaims(int userId);
    }
}
