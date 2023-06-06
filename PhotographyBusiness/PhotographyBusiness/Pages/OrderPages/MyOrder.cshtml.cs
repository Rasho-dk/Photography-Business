using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PhotographyBusiness.Models;
using PhotographyBusiness.Services.BookingService;
using PhotographyBusiness.Services.OrderService;
using PhotographyBusiness.Services.PhotoService;
using PhotographyBusiness.Services.UserService;

namespace PhotographyBusiness.Pages.OrderPages
{
    public class MyOrderModel : PageModel
    {
        private OrderService orderService;
        private IUserService userService;
        private IPhotoService photoService;

        [BindProperty]
        public IEnumerable<Order> Orders { get; set; }  
        public User User { get; set; }
        public MyOrderModel(OrderService orderService, IUserService userService,IPhotoService photoService)
        {
            this.orderService = orderService;
            this.userService = userService;
            this.photoService = photoService;
        }

        public IActionResult OnGet()
        {
            User = userService.GetUserByNameAsync(HttpContext.User.Identity.Name).Result;
           Orders = orderService.GetOrderWithBookingByUserId(User.UserId).Result;
            return Page();
        }
     
    }
}
