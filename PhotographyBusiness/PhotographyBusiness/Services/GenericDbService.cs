namespace PhotographyBusiness.Services
{
    public class GenericDbService<T> : IService<T> where T : class
    {
        public Task AddObjectAsync(T obj)
        {
            throw new NotImplementedException();
        }

        public Task DeleteObjectAsync(T obj)
        {
            throw new NotImplementedException();
        }

        public Task<T> GetObjectByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<T>> GetObjectsAsync()
        {
            throw new NotImplementedException();
        }

        public Task SaveObjects(List<T> objects)
        {
            throw new NotImplementedException();
        }

        public Task UpdateObjectAsync(T obj)
        {
            throw new NotImplementedException();
        }
    }
}
