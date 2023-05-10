using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PhotographyBusiness.Models;
using PhotographyBusiness.Services.BookingService;
using System.Data;

namespace PhotographyBusiness.Pages.BookingPages
{
    [Authorize(Roles = "admin")]
    public class AcceptRequestPageModel : PageModel
    {

        private IBookingService _bookingService;


        public Booking Booking { get; set; }
        public double Price { get; set; }
        public string AdminNote { get; set; }

        public AcceptRequestPageModel(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        public IActionResult OnGet(int id, double price, string note)
        {

            Booking = _bookingService.GetBookingById(id);
            Price = price;
            AdminNote = note;
            return Page();
        }

        public IActionResult OnPost(int id, double price, string note)
        {
            Booking = _bookingService.GetBookingById(id);

            if(Booking != null)
            {
                Booking.IsAccepted = true;
                Booking.Price = price;
                Booking.AdminNote = note;
                _bookingService.AcceptBooking(Booking.BookingId);
                return RedirectToPage("GetAllBookingRequestsPage");
            } 
            return RedirectToPage("NotFound");
        }

    }
}
