 using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Razor;
using PhotographyBusiness.Models;
using PhotographyBusiness.Services.BookingService;
using PhotographyBusiness.Services.UserService;
using System.ComponentModel.DataAnnotations;

namespace PhotographyBusiness.Pages.BookingPages
{
    public class CreateBookingRequestPageModel : PageModel
    {
        private IBookingService _bookingService;
        private IUserService _userService;

        [BindProperty] 
        public string City { get; set; }
        [BindProperty] 
        public string ZipCode { get; set; }
        [BindProperty] 
        public string Street { get; set; }
        [BindProperty] 
        public string CustomerNote { get; set; }
        [BindProperty] 
        public string Category { get; set; }
        [BindProperty, DataType(DataType.DateTime)]
        public DateTime Date { get; set; }
        public Booking Booking { get; set; } = new Booking();
        public User User { get; set; }

        public CreateBookingRequestPageModel(IBookingService bookingService, IUserService userService)
        {
            _bookingService = bookingService;
            _userService = userService;
        }

        public User GetUser()
        {
            return User;
        }

        public IActionResult OnGet()
        {
            if (HttpContext.User.Identity.Name == null)
            {
                return RedirectToPage("/AccountPages/LogInPage");
            }
            
            User = _userService.GetUserByNameAsync(HttpContext.User.Identity.Name).Result;
            //User = _userService.GetUserByEmailAsync(HttpContext.User.Identity.Name).Result;
            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            User = _userService.GetUserByNameAsync(HttpContext.User.Identity.Name).Result;

            Booking.Category = Category;
            Booking.CustomerNote = CustomerNote;
            Booking.Address = $"{Street}, {City} {ZipCode}";
            Booking.UserId = User.UserId;
            Booking.IsAccepted = false;
            Booking.Date = Date;
            Booking.DateCreated = DateTime.Now;

            await _bookingService.CreateBookingAsync(Booking);
            return RedirectToPage("../Index");
        }
    }
}
