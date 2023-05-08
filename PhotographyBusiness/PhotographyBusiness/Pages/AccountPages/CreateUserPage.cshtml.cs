using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PhotographyBusiness.Services.UserService;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PhotographyBusiness.Pages.AccountPages
{
    public class CreateUserPageModel : PageModel
    {
        private IUserService _userService;
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

        public CreateUserPageModel(IUserService userService)
        {
            _userService = userService;
            passwordHasher = new PasswordHasher<string>();
        }

        public IActionResult OnPost()
        {
            if (Password == RepeatPassword)
            {
                if (ModelState.IsValid)
                {
                    //_userService.CreateUser(new Models.User(Email, passwordHasher.HashPassword(null, Password), FullName, PhoneNumber));

                    // Shero: Jeg har brugt det kun for at lave unit test p� den..Den er uden HashPassword
                    _userService.CreateUserAsyn(new Models.User(Email, Password, $"{FirstName} {LastName}", PhoneNumber));

                    return RedirectToPage("../Index");
                }
                return Page();
                
            }
            return Page();
        }
    }
}