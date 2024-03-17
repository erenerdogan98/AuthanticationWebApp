using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AuthanticationWebAPI.Models.Context
{
    public class Context(DbContextOptions options) : IdentityDbContext(options)
    {

        public DbSet<Employee> Employees { get; set; }
    }
}
