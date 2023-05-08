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

        public void OnGet() // NOTE: De fleste metoder skal overf�res til de passende services!
        {
            Requests = _bookingService.GetMostRecentRequests().Result; // Get 5 most recent bookings requests
            Bookings = _bookingService.GetUpcomingBookings().Result.Take(5).OrderBy(b => b.Date).ToList(); // Get top 5 upcoming bookings
            Users = _userService.GetAllUsers().Take(5).ToList(); // Get 5 newest order by newest
            TotalUsers = _userService.GetAllUsers().Count();
            TotalBookings = _bookingService.GetAllBookings().Where(b => b.IsAccepted == true).ToList().Count(); // Total bookings
            BookingsThisMonth = _bookingService.GetAllBookingsThisMonth().Result.Count(); // Bookings last 30 days
        }
    }
}