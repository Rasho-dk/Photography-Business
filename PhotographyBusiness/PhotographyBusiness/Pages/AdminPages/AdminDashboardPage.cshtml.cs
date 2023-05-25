using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PhotographyBusiness.Models;
using PhotographyBusiness.Services;
using PhotographyBusiness.Services.BookingService;
using PhotographyBusiness.Services.UserService;

namespace PhotographyBusiness.Pages.AdminPages
{
    [Authorize(Roles = "admin")]
    public class AdminDashboardPageModel : PageModel
    {
        private IUserService _userService;
        private IBookingService _bookingService;
        private GenericDbService<User> _genericDbService; // Using DB service to get latest stats

        public List<Booking> Requests { get; set; }
        public List<Booking> Bookings { get; set; }
        public List<User> Users { get; set; }
        public int TotalUsers { get; set; }
        public int TotalBookings { get; set; }
        public int BookingsThisMonth { get; set; }
        public int CompletedBookingsThisMonth { get; set; }
        public int PendingRequests { get; set; }

        public AdminDashboardPageModel(IUserService userService, IBookingService bookingService, GenericDbService<User> genericDbService)
        {
            _userService = userService;
            _bookingService = bookingService;
            _genericDbService = genericDbService;
        }

        public void OnGet()
        {
            Requests = _bookingService.GetMostRecentRequests(); // Get 5 most recent bookings requests
            Bookings = _bookingService.GetUpcomingBookings().Take(5).OrderBy(b => b.Date).ToList(); // Get top 5 upcoming bookings
            Users = _genericDbService.GetObjectsAsync().Result.OrderByDescending(u => u.DateCreated).Take(5).ToList(); // Get 5 newest users
            TotalUsers = _genericDbService.GetObjectsAsync().Result.Count();
            TotalBookings = _bookingService.GetAllBookingsAsync().Result.Where(b => b.IsAccepted == true).ToList().Count(); // Total bookings
            BookingsThisMonth = _bookingService.GetAllBookingsThisMonth().Count(); // Bookings last 30 days
            CompletedBookingsThisMonth = _bookingService.GetAllBookingsAsync().Result.Where(b => b.Date < DateTime.Now && b.IsAccepted == true).ToList().Count(); // Completed bookings this month
            PendingRequests = _bookingService.GetAllBookingsRequests().Count();
        }
    }
}
