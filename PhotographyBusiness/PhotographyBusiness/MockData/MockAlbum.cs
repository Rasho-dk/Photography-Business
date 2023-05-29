using PhotographyBusiness.Models;

namespace PhotographyBusiness.MockData
{
    public class MockAlbum
    {
        private static List<Album> albums = new List<Album>()
        {
            new Album("Rasho","Wedding",11){Id = 1 }
        }; 
        public static List<Album> GetAlbums()
        {
            return albums;
        } 
    }
}
