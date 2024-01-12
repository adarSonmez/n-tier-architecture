using Domain.Dtos;

namespace Business.Abstract
{
    public interface IAuthService
    {
        void Register(RegisterAuthDto authDto);
        void Login(LoginAuthDto authDto);
    }
}
