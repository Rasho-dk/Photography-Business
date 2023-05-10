using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MimeKit.Encodings;
using PhotographyBusiness.Models;
using PhotographyBusiness.Services.BookingService;
using System.ComponentModel.DataAnnotations;

namespace PhotographyBusiness.Pages.BookingPages
{
    public class GetAllBookingsModel : PageModel
    {
        private IBookingService bookingService;
        public List<Booking> bookings;

        public BookingService booking { get; set; } 

        [BindProperty, DataType(DataType.Date)]
        public DateTime StartDate { get; set; } = DateTime.Now.AddMilliseconds(-DateTime.Now.Millisecond)
            .AddSeconds(-DateTime.Now.Second).AddMinutes(-DateTime.Now.Minute).AddHours(-DateTime.Now.Hour).AddHours(12);
        [BindProperty, DataType(DataType.Date)]
        public DateTime EndDate { get; set; } = DateTime.Now.AddMilliseconds(-DateTime.Now.Millisecond)
            .AddSeconds(-DateTime.Now.Second).AddMinutes(-DateTime.Now.Minute).AddHours(-DateTime.Now.Hour).AddHours(12).AddMonths(6);

        [BindProperty]
        public string NameInput { get; set; }

        [BindProperty]
        public string CategoryInput { get; set; }
       

        public GetAllBookingsModel(IBookingService bookingService)
        {
            this.bookingService = bookingService;
        }

        public async Task<IActionResult> OnGet()
        {
           bookings = bookingService.GetAllBookings().Where(x => x.IsAccepted == true).ToList(); 
            return Page();  
        }

        public async Task<IActionResult> OnGetSortBookingByCategory()
        {
            bookings = bookingService.SortBookingByCategory().Result;
            return Page();
        }

        public async Task<IActionResult> OnGetSortBookingByDate()
        {
            bookings = bookingService.SortBookingByDate().Result;
            return Page();
        }

        public async Task<IActionResult> OnGetSortBookingByName()
        {
            bookings = bookingService.SortBookingByName().Result;
            return Page();
        }

        public async Task<IActionResult> OnGetSortBookingByEmail()
        {
            bookings = bookingService.SortBookingByEmail().Result;
            return Page();
        }

        public async Task <IActionResult> OnPostDateFilter()
        {
            bookings = bookingService.FilterBookingsByDate(StartDate, EndDate).Result;
            return Page();
        }

        public async Task<IActionResult> OnPostCategoryFilter()
        {
            bookings = bookingService.FilterBookingsByCategory(CategoryInput).Result;
            return Page();

        }

        public async Task<IActionResult> OnpostNameSearch()
        {
            bookings = bookingService.FilterBookingsByNameOrEmail(NameInput).Result;
            return Page();
        }


    }
}
