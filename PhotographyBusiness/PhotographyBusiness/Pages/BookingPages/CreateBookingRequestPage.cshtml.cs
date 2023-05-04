using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PhotographyBusiness.Models;
using PhotographyBusiness.Services.BookingService;
using PhotographyBusiness.Services.UserService;

namespace PhotographyBusiness.Pages.BookingPages
{
    public class CreateBookingRequestPageModel : PageModel
    {
        private IBookingService _bookingService;
        private IUserService _userService;
        [BindProperty] public string Name { get; set; }
        [BindProperty] public string Email { get; set; }
        [BindProperty] public string Phone { get; set; }
        [BindProperty] public string City { get; set; }
        [BindProperty] public string ZipCode { get; set; }
        [BindProperty] public string StreetName { get; set; }
        public Booking Booking { get; set; }
        public User User { get; set; }

        public CreateBookingRequestPageModel(IBookingService bookingService, IUserService userService)
        {
            _bookingService = bookingService;
            _userService = userService;
        }

        public async IActionResult OnGet()
        {
            var user = await _userService.GetUserByEmailAsync(User.Email);
            return Page();
        }
    }
}
