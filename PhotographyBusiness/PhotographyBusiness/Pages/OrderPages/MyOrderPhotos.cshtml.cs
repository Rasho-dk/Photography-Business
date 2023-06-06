using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PhotographyBusiness.Models;
using PhotographyBusiness.Services.OrderService;
using PhotographyBusiness.Services.PhotoService;

namespace PhotographyBusiness.Pages.OrderPages
{
    public class MyOrderPhotosModel : PageModel
    {
        private IPhotoService photoService;
        private IOrderPhotoService orderPhotoService;
        public List<Photo> Photos { get; set; }
        public List<OrderPhoto> OrderPhotos { get; set; }   
        public MyOrderPhotosModel(IPhotoService photoService, IOrderPhotoService orderPhotoService)
        {
            this.photoService = photoService;
            this.orderPhotoService = orderPhotoService;
        }

        public IActionResult OnGet(int orderid)
        {
            Photos = photoService.GetAll(orderid).Result.ToList();
            OrderPhotos = orderPhotoService.GetAllPhotos().Where(o => o.OrderId == orderid).ToList();

            return Page();
        }
     
    }
}
