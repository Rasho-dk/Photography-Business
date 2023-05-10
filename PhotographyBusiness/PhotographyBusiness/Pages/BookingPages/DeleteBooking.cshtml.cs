using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PhotographyBusiness.Models;
using PhotographyBusiness.Services.BookingService;

namespace PhotographyBusiness.Pages.BookingPages
{
    public class DeleteBookingModel : PageModel
    {
        private IBookingService _bookingService;

        [BindProperty] public Booking Booking { get; set; }

        public DeleteBookingModel(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        public IActionResult OnGet(int id)
        {
            Booking = _bookingService.GetBookingById(id);
            if(Booking == null)
            {
                return RedirectToPage("/NotFound");
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            if(_bookingService.GetBookingById(id) != null)
            {
                _bookingService.DeleteBooking(id);
                return RedirectToPage("/GetAllBookings");
            }
            return RedirectToPage("/NotFound");
        }
    }
}
