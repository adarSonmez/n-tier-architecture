using Business.Repositories.OperationClaimRepository;
using Business.Repositories.UserOperationClaimRepository.Constants;
using Business.Repositories.UserOperationClaimRepository.Validation.FluentValidation;
using Business.Repositories.UserRepository;
using Core.Aspects;
using Core.Utilities.Business;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete;
using DataAccess.Repositories.UserOperationClaimRepository;
using Domain.Entities.Concrete;

namespace Business.Repositories.UserOperationClaimRepository
{
    public class UserOperationClaimManager : IUserOperationClaimService
    {
        private readonly IUserOperationClaimDal _userOperationClaimDal;
        private readonly IOperationClaimService _operationClaimService;
        private readonly IUserService _userService;

        public UserOperationClaimManager(IUserOperationClaimDal userOperationClaimDal, IOperationClaimService operationClaimService,IUserService userService)
        {
            _userOperationClaimDal = userOperationClaimDal;
            _operationClaimService = operationClaimService;
            _userService = userService;
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
                IResult result = BusinessRules.Run(
                    IsOperationSetExistForUpdate(operationClaim),
                    IsUserExists(operationClaim.UserId),
                    IsOperationClaimExists(operationClaim.OperationClaimId));

                if (!result.Success)
                {
                    return result;
                }

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
                IResult result = BusinessRules.Run(
                    IsOperationSetExistForAdd(operationClaim),
                    IsUserExists(operationClaim.UserId),
                    IsOperationClaimExists(operationClaim.OperationClaimId));

                if (!result.Success)
                {
                    return result;
                }

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

        private IResult IsOperationSetExistForAdd(UserOperationClaim userOperationClaim)
        {
            var result = _userOperationClaimDal.Get(uoc => uoc.UserId == userOperationClaim.UserId && uoc.OperationClaimId == userOperationClaim.OperationClaimId);
            if (result != null)
            {
                return new ErrorResult(UserOperationClaimMessages.OperationClaimAlreadySet);
            }
            return new SuccessResult();
        }

        private IResult IsOperationSetExistForUpdate(UserOperationClaim userOperationClaim)
        {
            var currentUserOperation = _userOperationClaimDal.Get(uoc => uoc.Id == userOperationClaim.Id);

            if (currentUserOperation == null)
            {
                return new ErrorResult(UserOperationClaimMessages.NotFound);
            }

            return IsOperationSetExistForAdd(userOperationClaim);
        }

        private IResult IsOperationClaimExists(int id)
        {
            var result = _operationClaimService.GetById(id);
            if (!result.Success)
            {
                return new ErrorResult(UserOperationClaimMessages.OperationClaimNotFound);
            }
            return new SuccessResult();
        }

        private IResult IsUserExists(int id)
        {
            var result = _userService.GetByUserId(id);
            if (!result.Success)
            {
                return new ErrorResult(UserOperationClaimMessages.UserNotFound);
            }
            return new SuccessResult();
        }
    }
}
