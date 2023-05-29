using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PhotographyBusiness.Models;
using PhotographyBusiness.Services.BookingService;
using PhotographyBusiness.Services.UserService;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PhotographyBusiness.Pages.AccountPages
{
    public class CreateBookingAndUserModel : PageModel
    {
        private const string existingEmail = "This email is already exist";
        private const string confirmBooking = "Your booking request has been successfully sent!";
        private const string dateAlert = "The date must be from today onwards.";
        private IUserService userService;
        private IBookingService bookingService;
        private PasswordHasher<string> passwordHasher;

       
        public Models.User User { get; set; }
        public Booking Booking { get; set; }

        /// <summary>
        /// Part 1 to create User 
        /// </summary>
        [BindProperty]
        [Required(ErrorMessage = "Please enter your email address.")]
        [StringLength(30, ErrorMessage = "The email address must be no more than {1} characters long.")]
        [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", ErrorMessage = "Please enter a valid email address. exampel@exampel.com")]
        public string Email { get; set; }
        [BindProperty]
        [Required(ErrorMessage = "Please enter your password.")]
        [StringLength(12, MinimumLength = 8, ErrorMessage = "The password must be between 8 and 20 characters long.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [BindProperty, DataType(DataType.Password), DisplayName("Repeat password")]
        [Compare("Password", ErrorMessage = "The passwords do not match.")]
        public string RepeatPassword { get; set; }
        [Required]
        [StringLength(12)]
        [BindProperty]
        public string PhoneNumber { get; set; }
        [BindProperty, DisplayName("First name")]
        public string FirstName { get; set; }
        [BindProperty, DisplayName("Last name")]
        public string LastName { get; set; }

        /// <summary>
        /// Part 2 To create Booking
        /// </summary>
        [BindProperty]
        [DataType(DataType.Currency)]
        public double? Price { get; set; }
        [Required(ErrorMessage = "Please enter your full address.")]
        [BindProperty]
        public string Address { get; set; }
        [Required(ErrorMessage = "Please enter a note.")]
        [BindProperty]
        public string CustomerNote { get; set; }
        [BindProperty]
        [Required(ErrorMessage = "Please choose one of the following category.")]
        public string Category { get; set; }
        [Required(ErrorMessage = "Please enter date of the event.")]
        [BindProperty]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DateOfEvent { get; set; }


        public string DisplayAlert { get; set; }
        public string DisplayConfirm { get; set; }

        public CreateBookingAndUserModel(IBookingService bookingService, IUserService userService)
        {
            this.bookingService = bookingService;
            this.userService = userService;
            passwordHasher = new PasswordHasher<string>();
        }

        public void OnGet()
        {

        }
        /// <summary>
        /// 1.th tjekke om koden passer med reapeatPassword
        /// 2.th tjekke om Email eksistere allered i systemet, hvis ja : returnere en fejlmeddelelse.
        /// 3.th  tjekke om dato er bestilt fra dagsdato hvsi nej returnere en fejlmeddelelse.
        /// 4.th Hvis all overstår er korrket opret en User og en booking request og returnere en succes meddelelse 
        /// </summary>
        /// <returns>Hvis der optået fejl fremvises en fejlmeddelelse ellers fortsætter til 
        /// at oprette User og en booking request</returns>
        public async Task<IActionResult> OnPostAsync()
        {

            if (Password == RepeatPassword && Password != null)
            {
                foreach (var user in userService.GetAllUsers())
                {
                    if (user.Email == Email)
                    {
                        DisplayAlert = existingEmail;
                        return Page();

                    }
                    DateTime currentDate = DateTime.Now.Date;
                    if(currentDate > DateOfEvent)
                    {
                        ModelState.AddModelError("DateOfEvent", dateAlert);
                    }
                    if (!ModelState.IsValid)
                    {
                        return Page();
                    }

                }

                //User.UserId++;
                await userService.CreateUserAsync
                    (new Models.User(Email,
                    passwordHasher.HashPassword(null, Password),
                    $"{FirstName} {LastName}", PhoneNumber));

                var user1 = userService.GetUserByEmailAsync(Email);


                //Booking.BookingId++;
                Booking = new Booking();
                Booking.Category = Category;
                Booking.CustomerNote = CustomerNote;
                Booking.UserId = user1.Result.UserId;
                Booking.Address = Address;
                Booking.IsAccepted = false;
                Booking.Date = DateOfEvent;
                await bookingService.CreateBookingAsync(Booking);
                if (Booking is not null)
                {
                    DisplayConfirm = confirmBooking;
                    return Page();
                }

                // return RedirectToPage("../Index");  
            }

            return Page();
        }
    }
}
