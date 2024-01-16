using Core.Utilities.Results.Abstract;
using Domain.Entities.Concrete;

namespace Business.Repositories.OperationClaimRepository
{
    public interface IOperationClaimService
    {
        IResult Add(OperationClaim operationClaim);
        IDataResult<OperationClaim?> GetById(int id);
        IDataResult<List<OperationClaim>?> GetList();
        IResult Update(OperationClaim operationClaim);
        IResult Delete(OperationClaim operationClaim);
        IResult DeleteById(int id);
    }
}
