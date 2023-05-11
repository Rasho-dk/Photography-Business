using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PhotographyBusiness.Models;
using PhotographyBusiness.Services.BookingService;
using PhotographyBusiness.Services.UserService;
using System.Data;

namespace PhotographyBusiness.Pages.BookingPages
{
    [Authorize(Roles = "admin")]
    public class GetAllBookingRequestsPageModel : PageModel
    {

        private IBookingService _bookingService;
        private IUserService _userService;

        public List<Booking> BookingRequests { get; set; }

        public GetAllBookingRequestsPageModel(IBookingService bookingService, IUserService userService)
        {
            _bookingService = bookingService;
            _userService = userService;
        }

        public void OnGet()
        {
            BookingRequests = _bookingService.GetAllBookingsRequests();
        }
    }
}
