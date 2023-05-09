using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MimeKit.Encodings;
using PhotographyBusiness.Models;
using PhotographyBusiness.Services.BookingService;

namespace PhotographyBusiness.Pages.BookingPages
{
    public class GetAllBookingsModel : PageModel
    {
        private IBookingService bookingService;
        public List<Booking> bookings;

        public DateTime Date { get; set; }

       

        public GetAllBookingsModel(IBookingService bookingService)
        {
            this.bookingService = bookingService;
        }

        public async Task<IActionResult> OnGet()
        {
           bookings = bookingService.GetAllBookings().Where(x => x.IsAccepted == true).ToList(); 
            return Page();  
        }

        public IActionResult OnPostDateFilter()
        {
            bookings = bookingService.FilterBookingsByDate(Date).ToList();
            return Page();
        }
    }
}
