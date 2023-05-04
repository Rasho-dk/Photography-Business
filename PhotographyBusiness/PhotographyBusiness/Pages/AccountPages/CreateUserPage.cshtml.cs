using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PhotographyBusiness.Services.UserService;
using System.ComponentModel.DataAnnotations;

namespace PhotographyBusiness.Pages.AccountPages
{
    public class CreateUserPageModel : PageModel
    {
        private IUserService _userService;
        private PasswordHasher<string> passwordHasher;
        
        [BindProperty]
        public string Email { get; set; }
        [BindProperty, DataType(DataType.Password)]
        public string Password { get; set; }
        [BindProperty]
        public string PhoneNumber { get; set; }
        [BindProperty]
        public string FullName { get; set; }
        [BindProperty, DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

        public CreateUserPageModel(IUserService userService)
        {
            _userService = userService;
            passwordHasher = new PasswordHasher<string>();
        }

        public IActionResult OnPost()
        {
            if (Password == ConfirmPassword)
            {
                if (ModelState.IsValid)
                {
                    //_userService.CreateUser(new Models.User(Email, passwordHasher.HashPassword(null, Password), FullName, PhoneNumber));
                    
                    // Shero: Jeg har brugt det kun for at lave unit test på den..Den er uden HashPassword
                    _userService.CreateUser(new Models.User(Email,Password, FullName, PhoneNumber));

                    return RedirectToPage("../Index");
                }
                return Page();
                
            }
            return Page();
        }
    }
}
