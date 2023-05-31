using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PhotographyBusiness.Models;
using PhotographyBusiness.Services.AlbumService;
using PhotographyBusiness.Services.OrderService;
using PhotographyBusiness.Services.PhotoService;

namespace PhotographyBusiness.Pages.PhotoPages
{
    public class GetPhotosModel : PageModel
    {
        private IOrderPhotoService orderPhotoService;
        private IPhotoService _photoService;

        public OrderPhoto OrderPhoto { get; set; } = new OrderPhoto();
        public List<Photo> Photos { get;  set; }
        public Photo Photo { get; set; }    

        public GetPhotosModel(IPhotoService photoService, IOrderPhotoService orderPhotoService)
        {
            _photoService = photoService;
            this.orderPhotoService = orderPhotoService;
        }
   

        public IActionResult OnGet(int albumid, int bookingid)
        {
            Photos = _photoService.GetAllPhotos()
               .Where(p => p.AlbumId.Equals(albumid)).ToList();
            return Page();
        }
        public async Task<IActionResult> OnPost(int id)
        {
            Photo =  _photoService.GetPhotoById(id).Result;
            
            OrderPhoto.Photo = Photo;
            
           await orderPhotoService.AddPhotoToOrder(OrderPhoto);
            return Page();
        }
    }
}
