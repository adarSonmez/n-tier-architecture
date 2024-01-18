using Core.Entities.Concrete;

namespace Core.Utilities.Security.Jwt
{
    public interface ITokenHandler
    {
        Token CreateAccessToken(User user, List<OperationClaim> operationClaims);
    }
}
