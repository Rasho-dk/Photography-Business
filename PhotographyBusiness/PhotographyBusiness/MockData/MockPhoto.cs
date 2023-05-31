using PhotographyBusiness.Models;

namespace PhotographyBusiness.MockData
{
    public class MockPhoto
    {
        private static List<Photo> photos = new List<Photo>()
        {
        };
        public static List<Photo> GetPhotos() { return photos; }
    }
}
