using Core.DataAccess.EntityFramework;
using DataAccess.Context.EntityFramework;
using Domain.Entities.Concrete;

namespace DataAccess.Repositories.OperationClaimRepository
{
    public class EfOperationClaimDal : EfEntityRepositoryBase<OperationClaim, AppDbContext>, IOperationClaimDal
    {
    }
}
