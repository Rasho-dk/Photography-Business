using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PhotographyBusiness.Models;
using PhotographyBusiness.Services.AlbumService;
using PhotographyBusiness.Services.PhotoService;

namespace PhotographyBusiness.Pages.PhotoPages
{
    public class GetPhotosModel : PageModel
    {
        private readonly int _pageSize;

        private IPhotoService _photoService;
        public List<Photo> Photos { get;  set; }

        public GetPhotosModel(IPhotoService photoService)
        {
            _photoService = photoService;
        }
   

        public IActionResult OnGet(int albumid, int bookingid)
        {
            Photos = _photoService.GetAllPhotos()
               .Where(p => p.AlbumId.Equals(albumid)).ToList();
            return Page();
        }
    }
}
