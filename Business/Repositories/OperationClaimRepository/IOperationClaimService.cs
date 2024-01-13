using Domain.Entities.Concrete;

namespace Business.Repositories.OperationClaimRepository
{
    public interface IOperationClaimService
    {
        void Add(OperationClaim operationClaim);
    }
}
