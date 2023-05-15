using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
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
            User = _userService.GetUserByNameAsync(HttpContext.User.Identity.Name).Result;

            Booking = _bookingService.GetBookingById(id);

            //User = _userService.GetUserByIdAsyn(Booking.UserId).Result;
            
            if (Booking == null)
            {
                return RedirectToPage("/NotFound");
            }
            return Page();
        }

        /// <summary>
        /// Onpost metode til update booking 
        /// </summary>
        /// <returns>Sender tilbage til listen over alle bookings</returns>
        public async Task<IActionResult> OnPostAsync()
        {
            //if (!ModelState.IsValid)
            //{
            //    return Page();
            //}
            _bookingService.UpdateBooking(Booking);
            return RedirectToPage("GetAllBookings");
        }
    }
}
