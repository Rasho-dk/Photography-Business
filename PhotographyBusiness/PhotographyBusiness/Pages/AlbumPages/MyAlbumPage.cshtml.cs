using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PhotographyBusiness.Models;
using PhotographyBusiness.Services.AlbumService;
using PhotographyBusiness.Services.UserService;
using System.Linq;

namespace PhotographyBusiness.Pages.AlbumPages
{
    public class MyAlbumPageModel : PageModel
    {
        private IAlbumService albumService;
        private IUserService userService;
        public List<Album> Albums { get; set; }

        public MyAlbumPageModel(IAlbumService albumService, IUserService userService)
        {
            this.albumService = albumService;
            this.userService = userService;
        }

        public IActionResult OnGet(int id2)
        {
            if(HttpContext.User.Identity.Name is not null)
            {
                var userId = userService.GetUserByNameAsync(HttpContext.User.Identity.Name).Result.UserId; 
                Albums = albumService.GetAllAlbumsAsync().Where(a => a.BookingId.Equals(id2)).ToList();  
            }
            return Page();  
        }
    }
}