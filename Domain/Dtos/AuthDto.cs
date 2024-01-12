using Core.Domain.Interfaces;

namespace Domain.Dtos
{
    public class AuthDto: IDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
