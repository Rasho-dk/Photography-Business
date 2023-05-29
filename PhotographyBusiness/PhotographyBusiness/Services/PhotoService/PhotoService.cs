using PhotographyBusiness.Models;

namespace PhotographyBusiness.Services.PhotoService
{
    public class PhotoService : IPhotoService
    {
        private List<Photo> photos;
        public PhotoService()
        {
            photos = MockData.MockPhoto.GetPhotos();
        }

        public async Task AddPhotoAsync(Photo photo, string filePath)
        {
            photo.FilePath = filePath;
            photos.Add(photo);   
        }

        public List<Photo> GetAllPhotos()
        {
            return photos;
        }

        public Task<Photo> GetPhotoById()
        {
            throw new NotImplementedException();
        }
    }
}

