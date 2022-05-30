using Microsoft.EntityFrameworkCore;
using Platzi_Dotnet.Models;


namespace Platzi_Dotnet.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
        {       
        }

        public DbSet<Product> Product { get; set; }
    }
}
