using Business.Repositories.OperationClaimRepository.Constants;
using Business.Repositories.OperationClaimRepository.Validation.FluentValidation;
using Core.Aspects;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Repositories.OperationClaimRepository;
using Domain.Entities.Concrete;

namespace Business.Repositories.OperationClaimRepository
{
    public class OperationClaimManager : IOperationClaimService
    {
        private readonly IOperationClaimDal _operationClaimDal;

        public OperationClaimManager(IOperationClaimDal operationClaimDal)
        {
            _operationClaimDal = operationClaimDal;
        }

        [ValidationAspect(typeof(OperationClaimValidator))]
        public IResult Add(OperationClaim operationClaim)
        {
            try
            {
                _operationClaimDal.Add(operationClaim);
                return new SuccessResult(OperationClaimMessages.Added);
            }
            catch(Exception e)
            {
                return new ErrorResult(e.Message);
            }
        }

        public IResult Delete(OperationClaim operationClaim)
        {
            try
            {
                _operationClaimDal.Delete(operationClaim);
                return new SuccessResult(OperationClaimMessages.Deleted);
            }
            catch(Exception e)
            {
                return new ErrorResult(e.Message);
            }
        }

        public IDataResult<OperationClaim?> GetById(int id)
        {
            var operationClaim = _operationClaimDal.Get(oc => oc.Id == id);
            if (operationClaim == null)
            {
                return new ErrorDataResult<OperationClaim?>(OperationClaimMessages.NotFound);
            }

            return new SuccessDataResult<OperationClaim?>(operationClaim, OperationClaimMessages.Retrieved);
        }

        public IDataResult<List<OperationClaim>?> GetList()
        {
            var operationClaims = _operationClaimDal.GetAll();
            if (operationClaims == null)
            {
                return new ErrorDataResult<List<OperationClaim>?>(OperationClaimMessages.NotFound);
            }

            return new SuccessDataResult<List<OperationClaim>?>(operationClaims, OperationClaimMessages.Listed  );
        }

        [ValidationAspect(typeof(OperationClaimValidator))]
        public IResult Update(OperationClaim operationClaim)
        {
            try
            {
                _operationClaimDal.Update(operationClaim);
                return new SuccessResult(OperationClaimMessages.Updated);
            }
            catch(Exception e)
            {
                return new ErrorResult(e.Message);
            }
        }
    }
}
