using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.IdentityModel.Tokens;
using PhotographyBusiness.Models;
using PhotographyBusiness.Services.UserService;

namespace PhotographyBusiness.Pages.AccountPages
{
    [Authorize(Roles = "admin")]
    public class GetAllUsersPageModel : PageModel
    {
        private IUserService _userService;
        public List<User> Users { get; set; }
        public List<User> FilterData { get; set; }
        public string DisplayAlert { get; set; }

        public GetAllUsersPageModel(IUserService userService)
        {
            _userService = userService;
        }


        public void OnGet()
        {
            Users = _userService.GetAllUsers();
            FilterData = Users;
        }
        public void OnPostFilterUsers(string filterData)
        {
            Users = _userService.GetAllUsers();
            FilterData = string.IsNullOrEmpty(filterData) ? Users : Users
                .Where(data => data.Name.ToLower().Contains(filterData.ToLower()) ||
                data.PhoneNumber.Contains(filterData)).ToList();
            if (FilterData.IsNullOrEmpty())
            {
                DisplayAlert = "This user : " +$"{filterData}, " + "is not exit in system. Please try again";
            }
        }

        public IActionResult OnPostDelete(int id)
        {
            _userService.DeleteUserAsync(id);
            return RedirectToPage("../AccountPages/GetAllUsersPage");
        }

    }
}

