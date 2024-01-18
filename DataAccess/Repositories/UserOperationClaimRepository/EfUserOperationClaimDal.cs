using Core.DataAccess.EntityFramework;
using DataAccess.Context.EntityFramework;
using Core.Entities.Concrete;

namespace DataAccess.Repositories.UserOperationClaimRepository
{
    public class EfUserOperationClaimDal : EfEntityRepositoryBase<UserOperationClaim, AppDbContext>, IUserOperationClaimDal
    {
    }
}
