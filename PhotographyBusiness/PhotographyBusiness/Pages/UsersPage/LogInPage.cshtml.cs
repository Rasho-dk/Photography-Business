using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PhotographyBusiness.Models;
using PhotographyBusiness.Services.UserService;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace PhotographyBusiness.Pages.UsersPage
{
    public class LogIndPageModel : PageModel
    {
        public IUserService userService { get; set; }   
        //Kun  en bruger i brug
        public static User LoggedInUser { get; set; } = null;
        [BindProperty]
        [Required(ErrorMessage = "Please enter your email address.")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
        [StringLength(100, ErrorMessage = "The email address must be no more than {1} characters long.")]
        public string Email { get; set; }
        [BindProperty,DataType(DataType.Password)]
        [Required(ErrorMessage = "Please enter your password.")]
        [StringLength(20, MinimumLength = 8, ErrorMessage = "The password must be between 8 and 20 characters long.")]
        public string? Password { get; set; }    
        public string DisplayMessage { get; set; }
        public LogIndPageModel(IUserService userService)
        {
            this.userService = userService; 
        }
        public void OnGet()
        {

        }
        public async Task<IActionResult> OnPost()
        {
            List<User> users = userService.GetAllUsers();   
            foreach (var user in users)
            {
                if(user.Email.Equals(Email))
                {
                    var passwordHasher = new PasswordHasher<string>();  
                    if(passwordHasher.VerifyHashedPassword(null,user.Password,Password) == PasswordVerificationResult.Success)
                    {
                        LoggedInUser = user;
                        var claims = new List<Claim> { new Claim(ClaimTypes.Name,Email)};
                        if (Email.Equals("admin")) claims.Add(new Claim(ClaimTypes.Role, "admin"));
                        
                        var claimsIdentity = new ClaimsIdentity(claims,CookieAuthenticationDefaults.AuthenticationScheme);
                        //await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
                        return RedirectToAction("../Pages/Index");
                    }
                }    
            }
            DisplayMessage = "Error";
            return Page();
        }
    }
}
