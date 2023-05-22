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
        public PlaceHolderModel(IUserService userService, IBookingService bookingService)
        {
            this.userService = userService;
            this.bookingService = bookingService;
        }

        public async Task<IActionResult> OnGet(int id)
        {
            if(HttpContext.User.Identity.Name != "admin")
            {
                User = await userService.GetUserByNameAsync(HttpContext.User.Identity.Name);
                //Booking =  bookingService.GetBookingById(id);
                Booking = bookingService.GetBookingById_User(id);
                Deposit = Booking.Price / 2;
                Remaining = Deposit;

            }
            else
            {
                foreach(var user in userService.GetAllUsers())
                {
                    if(user.UserId == id)
                    {
                        User = userService.GetUserByIdAsync(user.UserId).Result;
                        Booking = bookingService.GetBookingById_User(id);
                        Deposit = Booking.Price / 2;
                        Remaining = Deposit;
                    }
                }

            }
           
            return Page();
        }
    }
}
