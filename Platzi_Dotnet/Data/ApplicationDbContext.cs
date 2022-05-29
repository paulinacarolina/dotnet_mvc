using Microsoft.EntityFrameworkCore;
<<<<<<< HEAD
using Platzi_Dotnet.Models;
=======
>>>>>>> b4fb2cf (Clase 4: Integracion de Entity Framework)

namespace WebApplication1.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
        {       
        }
<<<<<<< HEAD
        public DbSet<Product> Product { get; set; }
=======

>>>>>>> b4fb2cf (Clase 4: Integracion de Entity Framework)
    }
}
