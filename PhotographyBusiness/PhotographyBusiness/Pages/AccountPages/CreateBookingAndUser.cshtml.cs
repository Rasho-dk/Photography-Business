using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PhotographyBusiness.Models;
using PhotographyBusiness.Services.BookingService;
using PhotographyBusiness.Services.UserService;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Microsoft.AspNetCore.Identity;

namespace PhotographyBusiness.Pages.AccountPages
{
    public class CreateBookingAndUserModel : PageModel
    {
        private IUserService userService;
        private IBookingService bookingService;
        private PasswordHasher<string> passwordHasher;


        [BindProperty]
        public User User { get; set; }
        [BindProperty, DisplayName("first name")]
        public string FirstName { get; set; }
        [BindProperty, DisplayName("last name")]
        public string LastName { get; set; }
        [BindProperty]
        public Booking Booking { get; set; }  

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
        //[BindProperty, DataType(DataType.Password), DisplayName("password")]
        //public string Password { get; set; }
        //[BindProperty, DataType(DataType.Password), DisplayName("repeat password")]
        //[Compare("Password", ErrorMessage = "The passwords do not match.")]
        //public string RepeatPassword { get; set; }
        [BindProperty,DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DateOfEvent { get; set; }

        public CreateBookingAndUserModel(IBookingService bookingService,IUserService userService)
        {
            this.bookingService = bookingService;
            this.userService = userService;
            passwordHasher = new PasswordHasher<string>();
        }

        public void OnGet()
        {

        }
        public async Task<IActionResult> OnPostAsync()
        {
            if(User.Password == User.RepeatPassword && User.Password != null)
            {
              

                //User.UserId++;


               User.Name = $"{FirstName} {LastName}";
               User.Password = passwordHasher.HashPassword(null, User.Password)  ;
               await userService.CreateUserAsync(User);

                //Booking.BookingId++;
                Booking = new Booking();
                Booking.UserId = User.UserId;
                Booking.Category = Category;
                Booking.CustomerNote = CustomerNote;
                Booking.Address = $"{Street}, {City} ,{ZipCode}";
                Booking.IsAccepted = false;
                Booking.Date = DateOfEvent;
                Booking.DateCreated = DateTime.Now;

                if (!ModelState.IsValid)
                {
                    return Page();
                }
                await bookingService.CreateBookingAsync(Booking);
              
                return RedirectToPage("../Index");  
            }                        
            return Page();
        }
    }
}
