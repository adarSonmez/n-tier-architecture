using Business.Repositories.UserOperationClaimRepository.Constants;
using Business.Repositories.UserOperationClaimRepository.Validation.FluentValidation;
using Core.Aspects;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Repositories.UserOperationClaimRepository;
using Domain.Entities.Concrete;

namespace Business.Repositories.UserOperationClaimRepository
{
    public class UserOperationClaimManager : IUserOperationClaimService
    {
        private readonly IUserOperationClaimDal _userOperationClaimDal;

        public UserOperationClaimManager(IUserOperationClaimDal userOperationClaimDal)
        {
            _userOperationClaimDal = userOperationClaimDal;
        }

        public IResult Delete(UserOperationClaim operationClaim)
        {
            try
            {
                _userOperationClaimDal.Delete(operationClaim);
                return new SuccessResult(UserOperationClaimMessages.Deleted);
            }
            catch (Exception e)
            {
                return new ErrorResult(e.Message);
            }
        }

        public IDataResult<UserOperationClaim?> GetById(int id)
        {
            var result = _userOperationClaimDal.Get(uoc => uoc.Id == id);
            if (result == null)
            {
                return new ErrorDataResult<UserOperationClaim?>(UserOperationClaimMessages.NotFound);
            }

            return new SuccessDataResult<UserOperationClaim?>(result, UserOperationClaimMessages.Retrieved);
        }

        public IDataResult<List<UserOperationClaim>?> GetList()
        {
            var result = _userOperationClaimDal.GetAll();
            if (result == null)
            {
                return new ErrorDataResult<List<UserOperationClaim>?>(UserOperationClaimMessages.NotFound);
            }

            return new SuccessDataResult<List<UserOperationClaim>?>(result, UserOperationClaimMessages.Retrieved);
        }

        [ValidationAspect(typeof(UserOperationClaimValidator))]
        public IResult Update(UserOperationClaim operationClaim)
        {
            try
            {
                _userOperationClaimDal.Update(operationClaim);
                return new SuccessResult(UserOperationClaimMessages.Updated);
            }
            catch (Exception e)
            {
                return new ErrorResult(e.Message);
            }
        }

        [ValidationAspect(typeof(UserOperationClaimValidator))]
        public IResult Add(UserOperationClaim operationClaim)
        {
            try
            {
                _userOperationClaimDal.Add(operationClaim);
                return new SuccessResult(UserOperationClaimMessages.Added);
            }
            catch (Exception e)
            {
                return new ErrorResult(e.Message);
            }
        }

        public IResult DeleteById(int id)
        {
            try
            {
                var operationClaim = _userOperationClaimDal.Get(uoc => uoc.Id == id);
                if (operationClaim == null)
                {
                    return new ErrorResult(UserOperationClaimMessages.NotFound);
                }
                _userOperationClaimDal.Delete(operationClaim);
                return new SuccessResult(UserOperationClaimMessages.Deleted);
            }
            catch (Exception e)
            {
                return new ErrorResult(e.Message);
            }
        }
    }
}
