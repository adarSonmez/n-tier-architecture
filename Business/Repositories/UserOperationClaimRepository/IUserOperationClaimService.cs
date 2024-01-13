using Domain.Entities.Concrete;

namespace Business.Repositories.UserOperationClaimRepository
{
    public interface IUserOperationClaimService
    {
        void Add(UserOperationClaim userOperationClaim);
    }
}
