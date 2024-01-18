using Core.Entities.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Domain.Dtos
{
    public class RegisterAuthDto: IDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public IFormFile? Image { get; set; }
    }
}
