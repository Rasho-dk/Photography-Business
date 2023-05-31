using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PhotographyBusiness.Models;
using PhotographyBusiness.Services.OrderService;
using PhotographyBusiness.Services.PhotoService;

namespace PhotographyBusiness.Pages.OrderPages
{
    public class AddPhotoToOrderPhotoModel : PageModel
    {
        private IOrderPhotoService orderPhotoService;
        private IPhotoService photoService;
        public List<OrderPhoto> OrderPhotos { get; set; }

        public AddPhotoToOrderPhotoModel(IOrderPhotoService orderPhotoService, IPhotoService photoService)
        {
            this.orderPhotoService = orderPhotoService;
            this.photoService = photoService;
        }

        public void OnGet()
        {
        }
        public void OnPost()
        {

          
        }
    }
}
