using Core.DataAccess.EntityFramework;
using Core.Entities.Concrete;
using DataAccess.Context.EntityFramework;

namespace DataAccess.Repositories.OperationClaimRepository
{
    public class EfOperationClaimDal : EfEntityRepositoryBase<OperationClaim, AppDbContext>, IOperationClaimDal
    {
    }
}
