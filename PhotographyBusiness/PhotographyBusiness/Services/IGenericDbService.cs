﻿namespace PhotographyBusiness.Services
{
    public interface IGenericDbService<T>
    {
        Task<IEnumerable<T>> GetObjectsAsync();
        Task AddObjectAsync(T obj);
        Task DeleteObjectAsync(T obj);
        Task UpdateObjectAsync(T obj);
        Task<T> GetObjectByIdAsync(int id);
        Task SaveObjects(List<T> objects);
    }
}