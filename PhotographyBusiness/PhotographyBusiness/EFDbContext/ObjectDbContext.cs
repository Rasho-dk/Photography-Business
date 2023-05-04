using Microsoft.EntityFrameworkCore;
using PhotographyBusiness.Models;

namespace PhotographyBusiness.EFDbContext
{
    public class ObjectDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=PhotographerDB ;Integrated Security=True;Connect Timeout=30;");
        }
        // TODO Der skal oprettes DbSet<T> get set til at konekte med Database

        public DbSet<User> Users { get; set; }
        public DbSet<Booking> Bookings { get; set; }

    }
}
