using Core.Entities.Concrete;
using FluentValidation;

namespace Business.Repositories.OperationClaimRepository.Validation.FluentValidation
{
    public class OperationClaimValidator: AbstractValidator<OperationClaim>
    {
        public OperationClaimValidator()
        {
            RuleFor(u => u.Name).NotEmpty().WithMessage("Claim name cannot be empty");
        }
    }
}
