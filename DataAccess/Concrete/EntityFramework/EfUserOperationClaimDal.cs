using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Context;
using Domain.Entities.Concrete;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfUserOperationClaimDal: EfEntityRepositoryBase<UserOperationClaim, AppDbContext>, IUserOperationClaimDal
    {
    }
}
