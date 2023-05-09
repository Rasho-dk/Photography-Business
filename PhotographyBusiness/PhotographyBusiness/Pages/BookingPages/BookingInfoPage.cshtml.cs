using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PhotographyBusiness.Models;
using PhotographyBusiness.Services.UserService;

namespace PhotographyBusiness.Pages.BookingPages
{
    public class BookingInfoPageModel : PageModel
    {
        private IUserService userService;
        public User User { get; set; }
        public BookingInfoPageModel(IUserService userService)
        {
            this.userService = userService; 
            
        }

        public async Task<IActionResult> OnGet(int id)
        {
            User = await userService.GetUserByIdAsyn(id);

            return Page();
        }
    }
}
