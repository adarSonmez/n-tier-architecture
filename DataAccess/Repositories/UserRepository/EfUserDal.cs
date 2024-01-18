using Core.DataAccess.EntityFramework;
using Core.Entities.Concrete;
using DataAccess.Context.EntityFramework;

namespace DataAccess.Repositories.UserRepository
{
    public class EfUserDal : EfEntityRepositoryBase<User, AppDbContext>, IUserDal
    {
        public List<OperationClaim> GetClaims(int userId)
        {
            using var context = new AppDbContext();
            var result = from operationClaim in context.OperationClaims
                         join userOperationClaim in context.UserOperationClaims!
                             on operationClaim.Id equals userOperationClaim.OperationClaimId
                         where userOperationClaim.UserId == userId
                         select new OperationClaim { Id = operationClaim.Id, Name = operationClaim.Name };
            
            return result.OrderBy(claim => claim.Name).ToList();
        }
    }
}
