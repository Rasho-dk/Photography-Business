using Microsoft.EntityFrameworkCore;

namespace PhotographyBusiness.EFDbContext
{
    public class ObjectDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=PhotographerDB ;Integrated Security=True;Connect Timeout=30;");
        }

    }
}
