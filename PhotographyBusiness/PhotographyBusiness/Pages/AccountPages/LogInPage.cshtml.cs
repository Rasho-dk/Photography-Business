using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PhotographyBusiness.Models;
using PhotographyBusiness.Services.UserService;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace PhotographyBusiness.Pages.AccountPages
{
    public class LogInPageModel : PageModel
    {
        public IUserService userService;
        //Kun en bruger i brug
        //public static User LoggedInUser { get; set; } = null;
        [BindProperty]
        [Required(ErrorMessage = "Please enter your email address.")]
        [StringLength(100, ErrorMessage = "The email address must be no more than {1} characters long.")]
        [RegularExpression(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", ErrorMessage = "Please enter a valid email address. exampel@exampel.com")]
        public string Email { get; set; }
        [BindProperty, DataType(DataType.Password)]
        [Required(ErrorMessage = "Please enter your password.")]
        [StringLength(20, MinimumLength = 8, ErrorMessage = "The password must be between 8 and 20 characters long.")]
        public string? Password { get; set; }
        public string DisplayMessage { get; set; }
        public LogInPageModel(IUserService userService)
        {
            this.userService = userService;
        }
        #region
        /// <summary>
        /// Used for Unit test
        ///This is for log in a user with a given email og password
        /// </summary>
        /// <param name="email">Email of the user to log in</param>
        /// <param name="password">Password of the user to log in</param>
        /// <returns>True if the user successfully logged in, false otherwise</returns>
        //public bool Login(string email, string password)
        //{
        //    List<User> users = userService.GetAllUsers();
        //    foreach(var user in users) 
        //    {
        //        if(user.Email == email && user.Password == Password)
        //            return true;
        //    }
        //    return false;
        //}
        #endregion

        /// <summary>
        /// This method must first retrieve the list of the Users via _userService and checl whether the user with (Eamil,Password) exists in the lsit
        /// If the user email exist and the password is match,a new list of the Claim objects is created which is initialized with a new Claim containing UserName.
        /// And if the user is admin new claim containing a "admin" and add to the list of the Claim with ClaimTypes.Role.
        /// Then ClaimIdentity object is created with our new list of Claims and a SignIn is made with the CalimsIdentity.
        /// <returns>If Login pass redurect to page ("/Index"), otherwise errore message dispaly </returns>

        public async Task<IActionResult> OnPostAsync()
        {

            List<User> users = userService.GetAllUsers();
            foreach (var user in users)
            {
                 if (Email.ToLower() == user.Email.ToLower()) // Hvis man ville bruge email til Claim
                //if (user.Name.Equals(user.Name))
                {
                    var passwordHasher = new PasswordHasher<string>();
                    try
                    {
                        if (passwordHasher.VerifyHashedPassword(null, user.Password, Password) == PasswordVerificationResult.Success)
                       // if (user.Password.Equals(Password))
                        {
                            //LoggedInUser = user;
                            var claims = new List<Claim> { new Claim(ClaimTypes.Name, user.Name) }; // Ændret email til user.Name
                            if (user.Name.Equals("admin")) claims.Add(new Claim(ClaimTypes.Role, "admin"));  // Arun: Betyder så også at Jacks admin User kommer til at have navnet "Admin"
                                                                                                             // if (user.Email == "EXAMPLE@jacksphotography.co.uk") claims.Add(new Claim(ClaimTypes.Role, "admin")) <-- Hvis vi hellere ville bruge email.

                            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
                            return RedirectToPage("/Index");
                        }
                    }
                    catch (Exception ex)
                    {

                        DisplayMessage = "Invalid email or password.Please try again";
                    }
                }
            }
            DisplayMessage = "Invalid email or password.Please try again";

            return Page();
        }
    }
}
