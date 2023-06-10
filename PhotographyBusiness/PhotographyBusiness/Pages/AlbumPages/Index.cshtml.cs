using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PhotographyBusiness.Models;
using PhotographyBusiness.Services.AlbumService;
using System.Data;

namespace PhotographyBusiness.Pages.PhotoPages
{
    [Authorize(Roles = "admin")]

    public class GallAllPhotosModel : PageModel
    {
        private IAlbumService albumService;
        public List<Album> Albums { get; private set; }
        
        public GallAllPhotosModel(IAlbumService albumService)
        {
            this.albumService = albumService;
        }

        public IActionResult OnGet()
        {
            Albums = albumService.GetAllAlbumsAsync();
            return Page();
        }
        public async Task<IActionResult> OnPostDeleteAlbum(int albumid)
        {
          await albumService.DeleteAlbum(albumid);  

            return RedirectToPage("/AlbumPages/Index");  
        }
      
    }
}
