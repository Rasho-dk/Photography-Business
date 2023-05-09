using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PhotographyBusiness.Services.BookingService;
using PhotographyBusiness.Models;

namespace PhotographyBusiness.Pages.BookingPages
{
    public class BookingRequestModalPageModel : PageModel
    {

        private IBookingService _bookingService;
        
        public List<Booking> BookingRequests { get; set; }
        [BindProperty] public Booking Booking { get; set; }


        public BookingRequestModalPageModel(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        public void OnGet(int id)
        {
            BookingRequests = _bookingService.GetAllBookingRequests();
            Booking = _bookingService.GetBookingById(id).Result;
        }
    }
}
