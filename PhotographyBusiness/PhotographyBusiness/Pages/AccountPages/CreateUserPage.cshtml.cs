using MailKit;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PhotographyBusiness.Models;
using PhotographyBusiness.Services.UserService;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PhotographyBusiness.Pages.AccountPages
{
    public class CreateUserPageModel : PageModel
    {
        private IUserService _userService;
        private Services.MailService.IMailService _mailService;
        private PasswordHasher<string> passwordHasher;

        [BindProperty, DataType(DataType.EmailAddress), DisplayName("email")]
        public string Email { get; set; }
        [BindProperty, DataType(DataType.Password), DisplayName("password")]
        public string Password { get; set; }
        [BindProperty, DisplayName("phone number")]
        public string PhoneNumber { get; set; }
        [BindProperty, DataType(DataType.Password), DisplayName("repeat password")]
        [Compare("Password", ErrorMessage = "The passwords do not match.")]
        public string RepeatPassword { get; set; }
        [BindProperty, DisplayName("first name")]
        public string FirstName { get; set; }
        [BindProperty, DisplayName("last name")]
        public string LastName { get; set; }

        public CreateUserPageModel(IUserService userService, Services.MailService.IMailService mailService)
        {
            _userService = userService;
            _mailService = mailService;
            passwordHasher = new PasswordHasher<string>();
        }

        public IActionResult OnPost()
        {
            if (Password == RepeatPassword)
            {
                if (ModelState.IsValid)
                {
                    // Shero: Jeg har brugt det kun for at lave unit test på den..Den er uden HashPassword
                   //_userService.CreateUserAsync(new Models.User(Email, Password, $"{FirstName} {LastName}", PhoneNumber));

                    _userService.CreateUserAsync(new Models.User(Email, passwordHasher.HashPassword(Email, Password), $"{FirstName} {LastName}", PhoneNumber));
                    //_mailService.SendUserCreationEmail(Email, $"{FirstName} {LastName}");

                    return RedirectToPage("../Index");
                }
                return Page();
                
            }
            return Page();
        }
    }
}
