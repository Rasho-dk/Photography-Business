using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace PhotographyBusiness.Pages.AccountPages
{
    public class LogOutPageModel : PageModel
    {
        public void OnGet()
        {

            //LogInPageModel.LoggedInUser = null;

            //await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            //return RedirectToPage("/index");
        }
    }
}
