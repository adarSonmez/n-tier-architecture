using Core.Utilities.Results.Abstract;
using Domain.Dtos;
using Domain.Entities.Concrete;

namespace Business.Repositories.UserRepository
{
    public interface IUserService
    {
        IResult Add(RegisterAuthDto authDto);
        IDataResult<List<User>> GetList();
        IDataResult<User?> GetByEmail(string email);
        IDataResult<User?> GetByUserId(int userId);
        IResult Update(User user);
        IResult Delete(User user);
        IResult DeleteById(int id);
        IResult ChangePassword(UserChangePasswordDto userChangePasswordDto);
    }
}
