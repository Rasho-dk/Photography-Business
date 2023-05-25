using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PhotographyBusiness.Models;
using PhotographyBusiness.Services.UserService;

namespace PhotographyBusiness.Pages.AccountPages
{
    [Authorize(Roles = "admin")]
    public class GetAllUsersPageModel : PageModel
    {
        private IUserService _userService;
        public List<User> Users { get; set; }

        public GetAllUsersPageModel(IUserService userService)
        {
            _userService = userService;
        }


        public IActionResult OnGet()
        {
            Users = _userService.GetAllUsers();
            return Page();
        }
        public IActionResult OnPostDelete(int id) 
        {
            _userService.DeleteUserAsync(id);
            return RedirectToPage("../AccountPages/GetAllUsersPage");
        }
    }
}
