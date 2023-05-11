using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PhotographyBusiness.Models;
using PhotographyBusiness.Services.BookingService;
using PhotographyBusiness.Services.UserService;

namespace PhotographyBusiness.Pages.BookingPages
{
    public class DeleteBookingModel : PageModel
    {
        private IBookingService _bookingService;
        private IUserService _userService;

        [BindProperty] public Booking Booking { get; set; }
        [BindProperty] public User User { get; set; }

        public DeleteBookingModel(IBookingService bookingService, IUserService userService)
        {
            this._bookingService = bookingService;
            this._userService = userService;
        }

        public IActionResult OnGet(int id)
        {
            User = _userService.GetUserByIdAsyn(id).Result;
            Booking = _bookingService.GetBookingById_User(User.UserId);

            if (Booking == null)
            {
                return RedirectToPage("/NotFound");
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if(_bookingService.GetBookingById(Booking.BookingId) != null)
            {
                _bookingService.DeleteBooking(Booking.BookingId);
                return RedirectToPage("GetAllBookings");
            }
            return RedirectToPage("/NotFound");
        }
    }
}
