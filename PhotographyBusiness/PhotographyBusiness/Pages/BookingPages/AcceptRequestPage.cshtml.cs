using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PhotographyBusiness.Models;
using PhotographyBusiness.Services.BookingService;

namespace PhotographyBusiness.Pages.BookingPages
{
    public class AcceptRequestPageModel : PageModel
    {

        private IBookingService _bookingService;


        public Booking Booking { get; set; }

        public AcceptRequestPageModel(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        public void OnGet(int id)
        {

            Booking = _bookingService.GetBookingById(id).Result;

        }

        public IActionResult OnPost(int id)
        {
            Booking = _bookingService.GetBookingById(id).Result;

            if(Booking != null)
            {
                Booking.IsAccepted = true;
                _bookingService.AcceptBooking(id);
                return RedirectToPage("BookingPages/GetAllBookingRequests");
            } 
            return RedirectToPage("NotFound");
        }

    }
}
