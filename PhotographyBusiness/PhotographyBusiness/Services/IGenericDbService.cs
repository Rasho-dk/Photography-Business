namespace PhotographyBusiness.Services
{
    public interface IGenericDbService<T>
    {
        public Task<IEnumerable<T>> GetObjectsAsync();
        public Task AddObjectAsync(T obj);
        public Task DeleteObjectAsync(T obj);
        public Task UpdateObjectAsync(T obj);
        public Task<T> GetObjectByIdAsync(int id);
        public Task SaveObjects(List<T> objects);

    }
}
