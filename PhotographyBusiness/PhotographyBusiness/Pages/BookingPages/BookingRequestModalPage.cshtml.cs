using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PhotographyBusiness.Services.BookingService;
using PhotographyBusiness.Models;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace PhotographyBusiness.Pages.BookingPages
{
    [Authorize(Roles = "admin")]
    public class BookingRequestModalPageModel : PageModel
    {

        private IBookingService _bookingService;
        
        public List<Booking> BookingRequests { get; set; }
        [BindProperty] public Booking Booking { get; set; }
        [BindProperty] public double Price { get; set; }    
        [BindProperty] public string AdminNote { get; set; }

        // asp-route virker ikke i HTML. Input bliver ikke bundet til property.



        public BookingRequestModalPageModel(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        public IActionResult OnGet(int id)
        {
            BookingRequests = _bookingService.GetAllBookingsRequests();
            Booking = _bookingService.GetBookingById(id);
            return Page();
        }

        public IActionResult OnPost()
        {
            
            return RedirectToPage("/BookingPages/AcceptRequestPage");
        }

        
    }
}
