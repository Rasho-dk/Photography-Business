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
                // if (Email == user.Email) // Hvis man ville bruge email til claim.
                if(user.Name.Equals(user.Name)) // Arun: Name istedet for email, da det ser lidt pænere ud at have admin som Claim og ikke Jacks Email // 
                   
                {
                    var passwordHasher = new PasswordHasher<string>();  
                    if(passwordHasher.VerifyHashedPassword(null,user.Password,Password) == PasswordVerificationResult.Success)
                    {
                        LoggedInUser = user;
                        var claims = new List<Claim> { new Claim(ClaimTypes.Name, user.Name)}; // Ændret fra email til user.Name
                        if (user.Name.Equals("admin")) claims.Add(new Claim(ClaimTypes.Role, "admin")); // Arun: Betyder så også at Jacks admin User kommer til at have navnet "Admin"
                        // if (user.Email == "jack@jacksphotography.co.uk") claims.Add(new Claim(ClaimTypes.Role, "admin")) <-- Hvis vi hellere ville bruge email.
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
