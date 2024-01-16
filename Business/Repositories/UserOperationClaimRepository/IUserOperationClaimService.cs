using Core.Utilities.Results.Abstract;
using Domain.Entities.Concrete;

namespace Business.Repositories.UserOperationClaimRepository
{
    public interface IUserOperationClaimService
    {
        IResult Add(UserOperationClaim operationClaim);
        IDataResult<UserOperationClaim?> GetById(int id);
        IDataResult<List<UserOperationClaim>?> GetList();
        IResult Update(UserOperationClaim operationClaim);
        IResult Delete(UserOperationClaim operationClaim);
        IResult DeleteById(int id);
    }
}
