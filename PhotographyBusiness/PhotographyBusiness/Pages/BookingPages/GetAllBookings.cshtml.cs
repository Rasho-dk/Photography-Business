using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MimeKit.Encodings;
using PhotographyBusiness.Models;
using PhotographyBusiness.Services.BookingService;
using PhotographyBusiness.Services.UserService;
using System.ComponentModel.DataAnnotations;

namespace PhotographyBusiness.Pages.BookingPages
{
    [Authorize(Roles = "admin")]
    public class GetAllBookingsModel : PageModel
    {
        private IBookingService bookingService;
        private IUserService userService;
        public List<Booking> Bookings { get; set; }   
        public User User { get; set; }

        [BindProperty, DataType(DataType.Date)]
        public DateTime StartDate { get; set; } = DateTime.Now.AddMilliseconds(-DateTime.Now.Millisecond)
        .AddSeconds(-DateTime.Now.Second).AddMinutes(-DateTime.Now.Minute).AddHours(-DateTime.Now.Hour).AddHours(12);
        [BindProperty, DataType(DataType.Date)]
        public DateTime EndDate { get; set; } = DateTime.Now.AddMilliseconds(-DateTime.Now.Millisecond)
        .AddSeconds(-DateTime.Now.Second).AddMinutes(-DateTime.Now.Minute).AddHours(-DateTime.Now.Hour).AddHours(12).AddMonths(6);

        [BindProperty]
        public string NameInput { get; set; }

        [BindProperty]
        public string CategoryInput { get; set; }


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
