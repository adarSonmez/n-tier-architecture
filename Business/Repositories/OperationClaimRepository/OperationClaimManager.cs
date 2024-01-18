using Business.Repositories.OperationClaimRepository.Constants;
using Business.Repositories.OperationClaimRepository.Validation.FluentValidation;
using Core.Aspects;
using Core.Entities.Concrete;
using Core.Utilities.Business;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Repositories.OperationClaimRepository;

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
                IResult result = BusinessRules.Run(IsNameExistForAdd(operationClaim.Name));

                if (!result.Success)
                {
                    return result;
                }

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

        public IResult DeleteById(int id)
        {
            try
            {
                var operationClaim = _operationClaimDal.Get(oc => oc.Id == id);
                if (operationClaim == null)
                {
                    return new ErrorResult(OperationClaimMessages.NotFound);
                }
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
                IResult result = BusinessRules.Run(IsNameExistForUpdate(operationClaim));

                if (!result.Success)
                {
                    return result;
                }

                _operationClaimDal.Update(operationClaim);
                return new SuccessResult(OperationClaimMessages.Updated);
            }
            catch(Exception e)
            {
                return new ErrorResult(e.Message);
            }
        }

        private IResult IsNameExistForAdd(string name)
        {
            var operationClaim = _operationClaimDal.Get(oc => oc.Name == name);
            if (operationClaim != null)
            {
                return new ErrorResult(OperationClaimMessages.NameAlreadyExist);
            }
            return new SuccessResult();
        }

        private IResult IsNameExistForUpdate(OperationClaim operationClaim)
        {
            var operationClaimToCheck = _operationClaimDal.Get(oc => oc.Id == operationClaim.Id);
            if (operationClaimToCheck == null)
            {
                return new ErrorResult(OperationClaimMessages.NotFound);
            }
            var operationClaimWithName = _operationClaimDal.Get(oc => oc.Name == operationClaim.Name);
            if (operationClaimWithName != null && operationClaimWithName.Id != operationClaim.Id)
            {
                return new ErrorResult(OperationClaimMessages.NameAlreadyExist);
            }
            return new SuccessResult();
        }
    }   
}
