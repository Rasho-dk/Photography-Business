using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PhotographyBusiness.Models;
using PhotographyBusiness.Services.UserService;

namespace PhotographyBusiness.Pages.AccountPages
{
    public class GetAllUsersPageModel : PageModel
    {
        private IUserService _userService;
        public List<User> Users { get; set; }


        public void OnGet()
        {
        }
    }
}
