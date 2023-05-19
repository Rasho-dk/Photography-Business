using Microsoft.EntityFrameworkCore;
using PhotographyBusiness.Models;

namespace PhotographyBusiness.EFDbContext
{
    public class ObjectDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            #region Shero 
            optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Jack;Integrated Security=True;Connect Timeout=30;");
            #endregion

            //optionsBuilder.UseSqlServer("Data Source=saunders.database.windows.net;Initial Catalog=SaundersDB;User ID=adminjack;Password=Vgroupftw!;Connect Timeout=60;Encrypt=True;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");

        }
        // TODO Der skal oprettes DbSet<T> get set til at konekte med Database

        public DbSet<User> Users { get; set; }
        public DbSet<Booking> Bookings { get; set; }

    }
}
