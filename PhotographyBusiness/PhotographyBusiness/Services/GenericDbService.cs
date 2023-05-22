    using Microsoft.EntityFrameworkCore;
using PhotographyBusiness.EFDbContext;

namespace PhotographyBusiness.Services
{
    public class GenericDbService<T> : IGenericDbService<T> where T : class
    {
        //Metoden tilføje et nyt objekt til DbContext
        //SaveChangesAsync(): gemmer ændring i databasen
        public async Task AddObjectAsync(T obj)
        {
            using (var context = new ObjectDbContext())
            {
                context.Set<T>().Add(obj);
                await context.SaveChangesAsync();
            }
        }

        public async Task DeleteObjectAsync(T obj)
        {
            using (var context = new ObjectDbContext())
            {
                context.Set<T>().Remove(obj);
                await context.SaveChangesAsync();
            }
        }

        public async Task<T> GetObjectByIdAsync(int id)
        {
            using (var context = new ObjectDbContext())
            {
                return await context.Set<T>().FindAsync(id);
            }
        }

        public async Task<IEnumerable<T>> GetObjectsAsync()
        {
            using (var context = new ObjectDbContext())
            {
                return await context.Set<T>().AsNoTracking().ToListAsync();
            }
        }

        public async Task SaveObjects(List<T> objects)
        {
            using (var context = new ObjectDbContext())
            {
                foreach (T obj in objects)
                {
                    context.Set<T>().Add(obj);
                    //context.SaveChanges();
                    await context.SaveChangesAsync();
                }
                await context.SaveChangesAsync();
            }
        }

        public async Task UpdateObjectAsync(T obj)
        {
            using (var context = new ObjectDbContext())
            {
                context.Set<T>().Update(obj);
                await context.SaveChangesAsync();
            }
        }


    }
}
