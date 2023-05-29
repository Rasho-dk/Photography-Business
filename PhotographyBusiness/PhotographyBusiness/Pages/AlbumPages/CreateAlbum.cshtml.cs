using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PhotographyBusiness.Models;
using PhotographyBusiness.Services.AlbumService;
using PhotographyBusiness.Services.BookingService;

namespace PhotographyBusiness.Pages.AlbumPages
{
    public class CreateAlbumModel : PageModel
    {
        private IAlbumService _albumService;
        private IBookingService _bookingService;
        [BindProperty]
        public Album Album { get; set; }
        //[BindProperty]
        //public int BookingId { get; set; }
        public Booking Booking { get; set; }
        public CreateAlbumModel(IAlbumService albumService,IBookingService bookingService)
        {
            _albumService = albumService;
            _bookingService = bookingService;
        }
        public IActionResult OnGet(int id)
        {
            Booking = _bookingService.GetBookingById(id);
            return Page();
        }
        public IActionResult OnPost(int id)
        {
            ModelState.Remove("Album.Photos");
            ModelState.Remove("Album.Booking");

            if (!ModelState.IsValid)
            {
                return Page();
            }
            Album.BookingId = _bookingService.GetBookingById(id).BookingId;
            _ = _albumService.CreateAlbum(Album);

            // Opret albummappen
            //string albumFolder = Path.Combine(Directory.GetCurrentDirectory(), "Albums", Album.Name);
            //Directory.CreateDirectory(albumFolder);

            return RedirectToPage("/AlbumPages/Index");
        }

    }
}
