using PhotographyBusiness.Models;

namespace PhotographyBusiness.Services.PhotoService
{
    public interface IPhotoService
    {
        List<Photo> GetAllPhotos();
        Task<Photo> GetPhotoById(int id);
        Task AddPhotoAsync(Photo photo);
        Task<List<Photo>> GetAll(int orderid);

    }
}
