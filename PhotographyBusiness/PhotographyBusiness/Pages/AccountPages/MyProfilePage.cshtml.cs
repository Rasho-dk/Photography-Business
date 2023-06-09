using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PhotographyBusiness.Models;
using PhotographyBusiness.Services.UserService;

namespace PhotographyBusiness.Pages.AccountPages
{
    public class MyProfilePageModel : PageModel
    {
        private IUserService userService;
        [BindProperty]
        public Models.User User { get; set; }
        public string ConformUpdate { get; set; }

        public MyProfilePageModel(IUserService userService)
        {
            this.userService = userService;
        }
        
        public async Task<IActionResult> OnGetAsync()
        {
            User = userService.GetUserByNameAsync(HttpContext.User.Identity.Name).Result;
            return Page();
        }
        public async Task<IActionResult> OnPost()
        {
       
            if (!ModelState.IsValid)
            {
                return Page();
            }
            //User = userService.GetUserByNameAsync(HttpContext.User.Identity.Name).Result;

            await userService.UpdateUserAsync(User);
            ConformUpdate = "Profile is updated";
            return Page();  

        }
    }
}
