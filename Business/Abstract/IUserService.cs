﻿using Domain.Dtos;
using Domain.Entities.Concrete;

namespace Business.Abstract
{
    public interface IUserService
    {
        void Add(RegisterAuthDto authDto);
        List<User> GetList();
        User? GetByEmail(string email);
    }
}
