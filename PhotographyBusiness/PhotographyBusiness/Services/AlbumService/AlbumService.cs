using PhotographyBusiness.Models;

namespace PhotographyBusiness.Services.AlbumService
{
    public class AlbumService : IAlbumService
    {
        private List<Album> _albums;
        private IAlbumService _albumService;
        private GenericDbService<Album> genericDbService;
        public AlbumService(GenericDbService<Album> genericDbService)
        {
            //_albums = MockData.MockAlbum.GetAlbums();
            this.genericDbService = genericDbService;
            _albums = genericDbService.GetObjectsAsync().Result.ToList();
        }

        public async Task CreateAlbum(Album album)
        {
            _albums.Add(album);
            await genericDbService.AddObjectAsync(album);   
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

        public async Task<Album> GetAlbumByBookingId(int id)
        {
            foreach (var album in _albums)
            {
                if (album.BookingId.Equals(id))
                {
                    return album;
                }
            }
            return null;
        }
    }
}
