using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PhotographyBusiness.Models;
using PhotographyBusiness.Services.UserService;

namespace PhotographyBusiness.Pages.AccountPages
{
    public class DeleteUserPageModel : PageModel
    {
        private IUserService userService;
        [BindProperty] 
        public User User { get; set; }
        public DeleteUserPageModel(IUserService userService)
        {
            this.userService = userService; 
        }

        public  IActionResult OnGet(int id)
        {
            User = userService.GetUserByIdAsync(id).Result;
            return Page();
        }
        public async  Task<IActionResult> OnPostAsync()
        {
            User toBeDeleted = userService.DeleteUserAsync(User.UserId).Result;
   
            return RedirectToPage("/AccountPages/GetAllUsersPage");

        }
    }
}
