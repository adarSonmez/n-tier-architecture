using Core.Context;
using Core.Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Context.EntityFramework
{
    public class AppDbContext : DbContextBase
    {
        public DbSet<User>? Users { get; set; }
        public DbSet<UserOperationClaim>? UserOperationClaims { get; set; }
        public DbSet<OperationClaim>? OperationClaims { get; set; }
    }
}
