using PhotographyBusiness.Models;

namespace PhotographyBusiness.Services.AlbumService
{
    public class AlbumService : IAlbumService
    {
        private List<Album> _albums;
        private IAlbumService _albumService;
        public AlbumService()
        {
            _albums = MockData.MockAlbum.GetAlbums();
        }

        public async Task CreateAlbum(Album album)
        {
            _albums.Add(album);
        }

        public Task<List<Album>> GetAlbumByBookingId()
        {
            throw new NotImplementedException();
        }

        public Album? GetAlbumByIdAsync(int albumId)
        {
            foreach (var album in _albums)
            {
                if (albumId.Equals(album.Id))
                {
                    return album;
                }
            }
            return null;

        }

        public List<Album> GetAllAlbumsAsync()
        {
            return _albums;
        }

        Task<Album> IAlbumService.GetAlbumByBookingId()
        {
            throw new NotImplementedException();
        }
    }
}
