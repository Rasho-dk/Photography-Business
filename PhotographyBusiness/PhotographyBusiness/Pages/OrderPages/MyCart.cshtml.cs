using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PhotographyBusiness.Models;
using PhotographyBusiness.Services.OrderService;
using PhotographyBusiness.Services.UserService;

namespace PhotographyBusiness.Pages.OrderPages
{
    public class MyCartModel : PageModel
    {
        private const decimal TaxPrice = 1.25M; //25% moms
        //private const string TextAlert = "This order has already been confirmed";
        private IOrderPhotoService orderPhotoService;
        private OrderService orderService;
        private IUserService userService;
        public IEnumerable<OrderPhoto> OrderPhotos { get; set; }
        [BindProperty]
        public Order Order { get; set; }
        public decimal SumOfPrice { get; set; }
        public int AlbumId { get; set; }
        public int OrderId { get; set; }
        public string AlertMessage { get; set; }
        
        //public User User { get; set; }
        public MyCartModel(IOrderPhotoService orderPhotoService,IUserService userService, OrderService orderService)
        {
            this.orderPhotoService = orderPhotoService;
            this.userService = userService;
            this.orderService = orderService;
        }
        /// <summary>
        /// Hente disse billede som matcher med OrderId. 
        /// </summary>
        /// <param name="orderid">Den for at sikke at billede tilhøre til den specifikke order</param>
        /// <returns></returns>
        public IActionResult OnGet(int orderid, int albumid)
        {
            AlbumId = albumid;
            OrderId = orderid;
           //User.UserId = userService.GetUserByNameAsync(HttpContext.User.Identity.Name).Result.UserId;
            OrderPhotos = orderPhotoService.GetAllPhotos().Where(o => o.OrderId == orderid).ToList();
            SumOfPrice = orderPhotoService.SumOfCalculatePrice(orderid);
            return Page();
        }
        /// <summary>
        /// Metod Sletter Order hvis kunden øsnker at slette det
        /// </summary>
        /// <param name="orderid">Order Id</param>
        /// <returns>Til MyBookingPage</returns>
        public async Task<IActionResult> OnPostCancelOrder(int orderid)
        {
           await orderService.DeleteOrderAsync(orderid);
            return RedirectToPage("/BookingPages/MyBookingPage");
        }
        /// <summary>
        /// Metoden slette uønskede produkt som ligger i kruven samt viser disse produkt som stadig ligger i kruv 
        /// </summary>
        /// <param name="orderphotoId"> OrderPhtotId: Id route for at slette når det paser i OrderPhoto Tabel</param>
        /// <param name="orderid">orderId Den skal route videre for at kalde på OnGet agin for at hente de billede som ligge i Cart og paser med OrderId(Fk)</param>
        /// <returns>Til det samme page MyCart</returns>
        public async Task<IActionResult> OnPostDeleteOrderPhoto(int orderphotoId,int orderid)
        {
            await orderPhotoService.RemovePhotoFromList(orderphotoId);
            return RedirectToPage("/OrderPages/MyCart");
        }
        public async Task<IActionResult> OnPostConfirmOrder(int orderid)
        {
            var orderToBeConfirm = orderService.GetOrderById(orderid);
            if(orderToBeConfirm.Condition is true)
            {
                //AlertMessage = TextAlert;
                return Page();
            }
            SumOfPrice = orderPhotoService.SumOfCalculatePrice(orderid);

            if (orderToBeConfirm is not null)
            {
                orderToBeConfirm.Condition = true;
                orderToBeConfirm.TotalPriceWithTax = SumOfPrice * TaxPrice;
                await orderService.ConfirmOrder(orderToBeConfirm);
            }
            TempData["OrderConfirmation"] = "The order has been created";

            orderPhotoService.BasketClean(orderid);
          

            return RedirectToPage("/BookingPages/MyBookingPage", new { TempData.Values });
        }

    }
}
