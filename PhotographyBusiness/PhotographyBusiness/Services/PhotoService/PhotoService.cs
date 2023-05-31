using Microsoft.EntityFrameworkCore;
using PhotographyBusiness.EFDbContext;
using PhotographyBusiness.Models;

namespace PhotographyBusiness.Services.PhotoService
{
    public class PhotoService : IPhotoService
    {
        private GenericDbService<Photo> dbphotoService;
        private List<Photo> photos;
        public PhotoService(GenericDbService<Photo> dbphotoService)
        {
            //photos = MockData.MockPhoto.GetPhotos();
            photos = dbphotoService.GetObjectsAsync().Result.ToList();  
            this.dbphotoService = dbphotoService;
        }
        //public async Task<List<Photo>> GetAllPhotoAsyncIncludeAlbum()
        //{
        //    using(var context = new ObjectDbContext)
        //    {
        //        return await context.Photos.Include(a => a.Album).ToListAsync();
        //    }
        //}
        public async Task AddPhotoAsync(Photo photo)
        {
            photos.Add(photo);   
            await dbphotoService.AddObjectAsync(photo); 

        }

        public List<Photo> GetAllPhotos()
        {
            return photos;
        }

        public async Task<Photo> GetPhotoById(int id)
        {
            foreach (var photo in photos)
            {
                if(photo.Id == id)
                {
                    return photo;
                }
            }
            return null;
        }
    }
}

