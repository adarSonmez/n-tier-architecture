using Core.Context;
using Domain.Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete.EntityFramework.Context
{
    public class AppDbContext: DbContextBase
    {
        public DbSet<User>? Users { get; set; }
        public DbSet<UserOperationClaim>? UserOperationClaims { get; set; }
        public DbSet<OperationClaim>? OperationClaims { get; set; }
    }
}
