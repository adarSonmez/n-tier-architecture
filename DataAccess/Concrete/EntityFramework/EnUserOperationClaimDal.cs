﻿using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework.Context;
using Domain.Concrete;

namespace DataAccess.Concrete.EntityFramework
{
    public class EnUserOperationClaimDal: EfEntityRepositoryBase<UserOperationClaim, AppDbContext>, IUserOperationClaimDal
    {
    }
}