using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PhotographyBusiness.Models;
using PhotographyBusiness.Services.BookingService;
using PhotographyBusiness.Services.UserService;

namespace PhotographyBusiness.Pages.AdminPages
{
    public class AdminDashboardPageModel : PageModel
    {
        private IUserService _userService;
        private IBookingService _bookingService;

        public List<Booking> Requests { get; set; }
        public List<Booking> Bookings { get; set; }
        public List<User> Users { get; set; }
        public int TotalUsers { get; set; }
        public int TotalBookings { get; set; }
        public int BookingsThisMonth { get; set; }

        public AdminDashboardPageModel(IUserService userService, IBookingService bookingService)
        {
            _userService = userService;
            _bookingService = bookingService;
        }

        public void OnGet()
        {
            Requests = _bookingService.GetAllBookings().Where(b => b.IsAccepted == true).Take(5).ToList(); // Get most recent bookings requests
            Bookings = _bookingService.GetAllBookings().Where(b => b.IsAccepted == true).Take(5).ToList(); // Get upcoming bookings
            Users = _userService.GetAllUsers().Take(5).ToList(); // Get 5 newest order by newest
            TotalUsers = _userService.GetAllUsers().Count();
            TotalBookings = _bookingService.GetAllBookings().Where(b => b.IsAccepted == true).ToList().Count();
            BookingsThisMonth = _bookingService.GetAllBookings().Where(b => b.IsAccepted == false).ToList().Count(); // Placeholder for now
        }
    }
}
