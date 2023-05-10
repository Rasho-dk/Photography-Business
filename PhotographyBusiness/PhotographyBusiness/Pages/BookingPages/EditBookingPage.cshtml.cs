using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PhotographyBusiness.Models;
using PhotographyBusiness.Services.BookingService;
using PhotographyBusiness.Services.UserService;

namespace PhotographyBusiness.Pages.BookingPages
{
    public class EditBookingPageModel : PageModel
    {
        private IBookingService bookingService;
        private IUserService userService;
        public User User { get; set; }  
        public Booking Booking { get; set; }    
        public EditBookingPageModel(IBookingService bookingService, IUserService userService)
        {
            this.bookingService = bookingService;   
            this.userService = userService;
        }
        public IActionResult OnGet(int id)
        {
            User = userService.GetUserByIdAsyn(id).Result;
            Booking = bookingService.GetBookingById_User(User.UserId);

            if (User == null)
                return RedirectToPage("/Index");
            return Page();
        }
		public async Task<IActionResult> OnPost()
		{
			if (!ModelState.IsValid)
			{
				return Page();
			}
			await bookingService.UpdateBooking(Booking);
            return RedirectToPage("/Index");

		}
	}
}
