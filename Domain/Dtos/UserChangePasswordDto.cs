using Core.Domain.Interfaces;

namespace Domain.Dtos
{
    public class UserChangePasswordDto: IDto
    {
        public int UserId { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
    }
}
