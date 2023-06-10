using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PhotographyBusiness.Models;
using PhotographyBusiness.Services.OrderService;
using PhotographyBusiness.Services.UserService;

namespace PhotographyBusiness.Pages.OrderPages
{
    public class CreateOrderModel : PageModel
    {
        private OrderService orderService;
        private IUserService userService;

        public List<Order> Orders { get; set; }
        public CreateOrderModel(OrderService orderService, IUserService userService)
        {
            this.orderService = orderService;
            this.userService = userService;
        }
        /// <summary>
        /// Metoden opretter en order for at route Order Id inden den går videre til OrderPhoto som består af en kruv til billeder. 
        /// Samt kalder på Get
        /// </summary>
        /// <param name="bookingid">BookingId: bruges til at hente orderId som lige oprettet</param>
        /// <param name="albumid">AlbumId: den bruges til at route videre til at hente billede på den valgt album</param>
        /// <returns> Til billede side </returns>
        public async Task<IActionResult> OnGet(int bookingid, int albumid)
        {
            int orderId = 0;
            Order order = new Order();
            order.CreatedDate = DateTime.Now;
            order.BookingId = bookingid;
            await orderService.CreateOrder(order);
            //var userId = userService.GetUserByNameAsync(HttpContext.User.Identity.Name).Result.UserId;
            orderId = orderService.GetOrderWithBookingById(bookingid).Result.Id;
            orderService.StartAutoDeletionAsync(orderId);

            return RedirectToPage("/PhotoPages/GetPhotos", new { bookingid, albumid, orderId });
        }


    }
}
