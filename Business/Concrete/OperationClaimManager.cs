using Business.Abstract;
using DataAccess.Abstract;
using Domain.Concrete;

namespace Business.Concrete
{
    public class OperationClaimManager : IOperationClaimService
    {
        private readonly IOperationClaimDal _operationClaimDal;

        public OperationClaimManager(IOperationClaimDal operationClaimDal)
        {
            _operationClaimDal = operationClaimDal;
        }

        public void Add(OperationClaim operationClaim)
        {
            _operationClaimDal.Add(operationClaim);
        }
    }
}
