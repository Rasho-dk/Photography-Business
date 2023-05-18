using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PhotographyBusiness.Models;
using PhotographyBusiness.Services.BookingService;
using PhotographyBusiness.Services.UserService;
using System.ComponentModel.DataAnnotations;

namespace PhotographyBusiness.Pages.BookingPages
{
    public class PlaceHolderModel : PageModel
    {
        private IUserService userService;
        private IBookingService bookingService;

        public User User { get; set; }
        public Booking Booking { get; set; }
        [BindProperty]
        [DataType(DataType.Currency)]
        public double? Deposit { get; set; }
        [DataType(DataType.Currency)]
        public double? Remaining { get; set; }
        [BindProperty]
        public string newText { get; set; } 
        public PlaceHolderModel(IUserService userService, IBookingService bookingService)
        {
            this.userService = userService;
            this.bookingService = bookingService;
        }

        public async Task<IActionResult> OnGet(int id)
        {
            User = await userService.GetUserByNameAsync(HttpContext.User.Identity.Name);
            Booking =  bookingService.GetBookingById(id);
            Deposit = Booking.Price / 2;
            Remaining = Deposit;
            newText = "04-00-04";

            return Page();
        }
    }
}
