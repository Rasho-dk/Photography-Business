using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MimeKit.Encodings;
using PhotographyBusiness.Models;
using PhotographyBusiness.Services.BookingService;
using PhotographyBusiness.Services.UserService;

namespace PhotographyBusiness.Pages.BookingPages
{
    [Authorize(Roles = "admin")]
    public class GetAllBookingsModel : PageModel
    {
        private IBookingService bookingService;
        private IUserService userService;
        public List<Booking> Bookings { get; set; }   
        public User User { get; set; }

        public GetAllBookingsModel(IBookingService bookingService,IUserService userService)
        {
            this.bookingService = bookingService;
            this.userService = userService;
        }

        public IActionResult OnGet()
        {
            User = userService.GetUserByNameAsync(HttpContext.User.Identity.Name).Result;
            Bookings = bookingService.GetAllBookings().Where(x => x.IsAccepted == true).ToList();
            return Page();
        }
       

    }
}
