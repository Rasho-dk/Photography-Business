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
        [BindProperty]
        public DateTime DateInput { get; set; } = DateTime.Now.AddMilliseconds(-DateTime.Now.Millisecond)
            .AddSeconds(-DateTime.Now.Second).AddMinutes(-DateTime.Now.Minute).AddHours(-DateTime.Now.Hour).AddHours(12); 

        [BindProperty]
        public string SearchInput { get; set; }

       

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
            bookings = bookingService.FilterBookingsByDate(DateInput).ToList();
            return Page();
        }

        public IActionResult OnpostNameSearch()
        {
            bookings = bookingService.FilterBookingsByNameOrEmail(SearchInput).ToList();
            return Page();
        }

        public async Task<IActionResult> OnPostCateGorySearch()
        {
            bookings = bookingService.FilterBookingsByCategory(SearchInput).Result;
            return Page();
            
        }
    }
}
