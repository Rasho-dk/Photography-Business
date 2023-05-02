using Microsoft.EntityFrameworkCore;
using PhotographyBusiness.EFDbContext;

namespace PhotographyBusiness.Services
{
    public class GenericDbService<T> : IService<T> where T : class
    {
        public async Task AddObjectAsync(T obj)
        {
            using(var context = new ItemDbContext())
            {
                context.Set<T>().Add(obj);
                await context.SaveChangesAsync();
            }
        }

        public Task DeleteObjectAsync(T obj)
        {
            throw new NotImplementedException();
        }

        public Task<T> GetObjectByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<T>> GetObjectsAsync()
        {
            using(var context = new ItemDbContext())
            {
                return await context.Set<T>().AsNoTracking().ToListAsync();
            }
        }

        public async Task SaveObjects(List<T> objects)
        {
            using (var context = new ItemDbContext())
            {
                foreach(T obj in objects)
                {
                    context.Set<T>().Add(obj);
                    context.SaveChanges();
                }
                context.SaveChanges();
            }
        }

        public Task UpdateObjectAsync(T obj)
        {
            throw new NotImplementedException();
        }
    }
}
