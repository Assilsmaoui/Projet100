using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace TP7.Models
{
    public class TP7Context : IdentityDbContext<ApplicationUser>
    {

        public TP7Context(DbContextOptions optons) :base(optons) { }


        public DbSet<Departement> Departements { get; set; }
    }
}
