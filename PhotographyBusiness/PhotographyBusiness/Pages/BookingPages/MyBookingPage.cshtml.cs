using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PhotographyBusiness.Models;
using PhotographyBusiness.Services.BookingService;
using PhotographyBusiness.Services.UserService;

namespace PhotographyBusiness.Pages.BookingPages
{
    public class MyBookingPageModel : PageModel
    {
        private IBookingService bookingService;
        private IUserService userService;
        public User User { get; set; }
        public List<Booking> Bookings { get; set; }

        public MyBookingPageModel(IBookingService bookingService, IUserService userService)
        {
            this.bookingService = bookingService;
            this.userService = userService;
        }

        public async Task<IActionResult> OnGet()
        {
            User = userService.GetUserByNameAsync(HttpContext.User.Identity.Name).Result;
            Bookings = bookingService.GetBookingsByUserId(User.UserId);

            return Page();
        }

    }
}
