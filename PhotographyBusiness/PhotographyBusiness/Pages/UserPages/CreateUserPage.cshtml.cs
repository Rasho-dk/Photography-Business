using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace PhotographyBusiness.Pages.UserPages
{
    public class CreateUserPageModel : PageModel
    {
        
        private PasswordHasher<string> passwordHasher;
        
        [BindProperty]
        public string Email { get; set; }
        [BindProperty, DataType(DataType.Password)]
        public string Password { get; set; }
        [BindProperty]
        public string PhoneNumber { get; set; }
        [BindProperty]
        public string FullName { get; set; }

        public CreateUserPageModel()
        {
            passwordHasher = new PasswordHasher<string>();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            // UserService to create the new user
            return RedirectToPage("Index");
        }
    }
}
