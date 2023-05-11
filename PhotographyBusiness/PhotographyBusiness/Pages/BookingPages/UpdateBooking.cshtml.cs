using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PhotographyBusiness.Models;
using PhotographyBusiness.Services.BookingService;
using PhotographyBusiness.Services.UserService;

namespace PhotographyBusiness.Pages.BookingPages
{
    public class UpdateBookingModel : PageModel
    {
        private IBookingService _bookingService;
        private IUserService _userService;

        [BindProperty] public Booking Booking { get; set; }
        [BindProperty] public User User { get; set; }

        public UpdateBookingModel(IBookingService bookingService, IUserService userService)
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

        /// <summary>
        /// Onpost metode til update booking 
        /// </summary>
        /// <returns>Sender tilbage til listen over alle bookings. NotFound siden er ikke lavet og bare skrevet som default</returns>
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _bookingService.UpdateBooking(Booking);
            return RedirectToPage("GetAllBookings");
        }
    }
}
