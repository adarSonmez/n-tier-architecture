using Core.Utilities.Results.Abstract;
using Domain.Dtos;

namespace Business.Abstract
{
    public interface IAuthService
    {
        IResult Register(RegisterAuthDto authDto);
        IResult Login(LoginAuthDto authDto);
    }
}
