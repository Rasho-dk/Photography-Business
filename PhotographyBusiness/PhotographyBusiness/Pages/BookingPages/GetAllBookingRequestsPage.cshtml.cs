using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PhotographyBusiness.Models;
using PhotographyBusiness.Services.BookingService;
using PhotographyBusiness.Services.UserService;

namespace PhotographyBusiness.Pages.BookingPages
{
    public class GetAllBookingRequestsPageModel : PageModel
    {

        private IBookingService _bookingService;
        private IUserService _userService;

        public List<Booking> Bookings { get; set; }

        public GetAllBookingRequestsPageModel(IBookingService bookingService, IUserService userService)
        {
            _bookingService = bookingService;
            _userService = userService;
        }

        public void OnGet()
        {
            Bookings = _bookingService.GetAllBookings();
        }
    }
}
