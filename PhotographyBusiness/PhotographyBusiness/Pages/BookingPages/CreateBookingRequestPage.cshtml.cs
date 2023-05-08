using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Razor;
using PhotographyBusiness.Models;
using PhotographyBusiness.Services.BookingService;
using PhotographyBusiness.Services.UserService;


namespace PhotographyBusiness.Pages.BookingPages
{
    public class CreateBookingRequestPageModel : PageModel
    {
        private IBookingService _bookingService;
        private IUserService _userService;

        [BindProperty] public string City { get; set; }
        [BindProperty] public string ZipCode { get; set; }
        [BindProperty] public string Street { get; set; }
        [BindProperty] public string CustomerNote { get; set; }
        [BindProperty] public string Category { get; set; }

        public string FullAddress { get; set; }
        public Booking Booking { get; set; }
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

            Booking = new Booking();
            User = _userService.GetUserByEmailAsync(HttpContext.User.Identity.Name).Result;

            Booking.UserId = User.UserId;
            Booking.User = User;
            Booking.Address = City + ZipCode + Street;
            Booking.CustomerNote = CustomerNote;
            Booking.Category = Category;
            await _bookingService.CreateBooking(Booking);
            return RedirectToPage("Index");
        }
    }
}
