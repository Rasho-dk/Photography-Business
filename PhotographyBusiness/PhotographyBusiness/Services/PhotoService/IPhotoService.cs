using PhotographyBusiness.Models;

namespace PhotographyBusiness.Services.PhotoService
{
    public interface IPhotoService
    {
        List<Photo> GetAllPhotos();
        Task<Photo> GetPhotoById();
        Task AddPhotoAsync(Photo photo,string filePath);
    }
}
