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
        [Required(ErrorMessage = "Please enter your note.")]
        public string CustomerNote { get; set; }

        [Required(ErrorMessage = "Please enter your full address.")]
        [BindProperty]
        public string Address { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Please choose a category.")]
        public string Category { get; set; }

        [BindProperty, DataType(DataType.DateTime)]
        [Required(ErrorMessage = "Please enter date of the event.")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }

        public Booking Booking { get; set; } = new Booking();
        public User User { get; set; }

        //public string DisplayAlert { get; set; }
        //public string DisplayConfirm { get; set; }

        public CreateBookingRequestPageModel(IBookingService bookingService, IUserService userService)
        {
            _bookingService = bookingService;
            _userService = userService;
        }

        public IActionResult OnGet()
        {
            if (HttpContext.User.Identity.Name == null)
            {
                return RedirectToPage("/AccountPages/LogInPage");
            }
            
            User = _userService.GetUserByNameAsync(HttpContext.User.Identity.Name).Result;
            return Page();
        }

        /// <summary>
        /// OnPost
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> OnPost()
        {
            DateTime currentDate = DateTime.Now.Date;
            if (currentDate > Date)
            {
                ModelState.AddModelError("Date", "The date must be from today onwards.");
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }

            User = _userService.GetUserByNameAsync(HttpContext.User.Identity.Name).Result;
            Booking.Category = Category;
            Booking.CustomerNote = CustomerNote;
            Booking.Address = Address;
            Booking.UserId = User.UserId;
            Booking.IsAccepted = false;
            Booking.Date = Date;
            Booking.DateCreated = DateTime.Now;
            await _bookingService.CreateBookingAsync(Booking);
          

            return RedirectToPage("../Index");
        }
    }
}
