using Core.Utilities.Results.Abstract;
using Core.Utilities.Security.Jwt;
using Domain.Dtos;

namespace Business.Authentication
{
    public interface IAuthService
    {
        IResult Register(RegisterAuthDto authDto);
        IDataResult<Token?> Login(LoginAuthDto authDto);
    }
}
