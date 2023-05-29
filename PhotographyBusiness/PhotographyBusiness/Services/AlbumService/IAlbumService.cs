using PhotographyBusiness.Models;

namespace PhotographyBusiness.Services.AlbumService
{
    public interface IAlbumService
    {
        Album? GetAlbumByIdAsync(int albumId);
        List<Album> GetAllAlbumsAsync ();
        Task<Album> GetAlbumByBookingId();
        Task CreateAlbum(Album album);

    }
}
