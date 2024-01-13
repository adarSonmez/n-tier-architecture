using Core.DataAccess.EntityFramework;
using DataAccess.Context.EntityFramework;
using Domain.Entities.Concrete;

namespace DataAccess.Repositories.UserOperationClaimRepository
{
    public class EfUserOperationClaimDal : EfEntityRepositoryBase<UserOperationClaim, AppDbContext>, IUserOperationClaimDal
    {
    }
}
