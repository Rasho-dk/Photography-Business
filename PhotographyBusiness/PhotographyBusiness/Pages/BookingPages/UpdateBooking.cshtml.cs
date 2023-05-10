using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PhotographyBusiness.Models;
using PhotographyBusiness.Services.BookingService;

namespace PhotographyBusiness.Pages.BookingPages
{
    public class UpdateBookingModel : PageModel
    {
        private IBookingService _bookingService;

        public Booking Booking { get; set; }

        public UpdateBookingModel(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        public IActionResult OnGet(int id)
        {
            Booking = _bookingService.GetBookingById(id).Result;

            if(Booking == null)
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
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _bookingService.UpdateBooking(Booking);
            return RedirectToPage("/GetAllBookings");
        }
    }
}
