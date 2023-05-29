using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PhotographyBusiness.Models;
using PhotographyBusiness.Services.AlbumService;

namespace PhotographyBusiness.Pages.PhotoPages
{
    public class GallAllPhotosModel : PageModel
    {
        private IAlbumService albumService;
        public List<Album> Albums { get; private set; }
        private List<Photo> photos;
        
        public GallAllPhotosModel(IAlbumService albumService)
        {
            this.albumService = albumService;
        }

        public IActionResult OnGet()
        {
            Albums = albumService.GetAllAlbumsAsync();
            return Page();
        }
      
    }
}
