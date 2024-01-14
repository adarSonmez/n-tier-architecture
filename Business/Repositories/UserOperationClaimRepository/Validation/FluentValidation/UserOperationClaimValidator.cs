using Domain.Entities.Concrete;
using FluentValidation;

namespace Business.Repositories.UserOperationClaimRepository.Validation.FluentValidation
{
    public class UserOperationClaimValidator: AbstractValidator<UserOperationClaim>
    {
        public UserOperationClaimValidator()
        {
            RuleFor(uoc => uoc.UserId).NotEmpty().WithMessage("User id cannot be empty");
            RuleFor(uoc => uoc.OperationClaimId).NotEmpty().WithMessage("Operation claim id cannot be empty");
        }
    }
}
