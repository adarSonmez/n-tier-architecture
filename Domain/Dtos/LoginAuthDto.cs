﻿using Core.Entities.Interfaces;

namespace Domain.Dtos
{
    public class LoginAuthDto: IDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
