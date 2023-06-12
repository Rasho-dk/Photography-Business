using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PhotographyBusiness.Models;
using PhotographyBusiness.Services.BookingService;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace PhotographyBusiness.Pages.BookingPages
{
    [Authorize(Roles = "admin")]
    public class AcceptRequestPageModel : PageModel
    {

        private IBookingService _bookingService;


        public Booking Booking { get; set; }
        [BindProperty]
        [Required, Range(1, 999999)]
        public double Price { get; set; }
        [BindProperty]
        public string AdminNote { get; set; }

        public AcceptRequestPageModel(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        public IActionResult OnGet(int id)
        {

            Booking = _bookingService.GetBookingById(id);

            return Page();
        }

        public async Task<IActionResult> OnPost(int id)
        {
            Booking = _bookingService.GetBookingById(id);

            if(Booking != null)
            {
                if (ModelState.IsValid)
                {
                    Booking.IsAccepted = true;
                    Booking.Price = Price;
                    Booking.AdminNote = AdminNote;
                    await _bookingService.ConfirmBooking(Booking);
                    return RedirectToPage("GetAllBookingRequestsPage");
                }
            }
            return Page();
        }
        /// <summary>
        /// Here når admin trykker på Decline så sletter denne booking fra databasen og fra listen
        /// </summary>
        /// <returns>Redirect to page GetAllBookings </returns>
        public  IActionResult OnPostDelete(int id)
        {
            var toBeDeleted = _bookingService.DeleteBooking(id);

            return RedirectToPage("GetAllBookings");
        }

    }
}
