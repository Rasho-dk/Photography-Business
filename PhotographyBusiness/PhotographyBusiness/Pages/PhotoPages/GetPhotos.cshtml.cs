using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.IdentityModel.Tokens;
using PhotographyBusiness.Models;
using PhotographyBusiness.Services.AlbumService;
using PhotographyBusiness.Services.OrderService;
using PhotographyBusiness.Services.PhotoService;

namespace PhotographyBusiness.Pages.PhotoPages
{
    public class GetPhotosModel : PageModel
    {
        private const string alertMessage = "Please choose image size";
        private IOrderPhotoService orderPhotoService;
        private IPhotoService _photoService;
        private IAlbumService _albumService;


        [BindProperty]
        public OrderPhoto OrderPhoto { get; set; } = new OrderPhoto();
        public List<OrderPhoto> OrderPhotos { get; set; }
        public List<Photo> Photos { get; set; }
        public Photo Photo { get; set; }
        [BindProperty]
        public string Size { get; set; } = null;
        public string Message { get; set; }
        [BindProperty]
        public int OrderId { get; set; }
        [BindProperty]
        public int AlbumId { get; set; }
        public int CartQuantity { get; set; }


        public GetPhotosModel(IPhotoService photoService, IOrderPhotoService orderPhotoService,
              IAlbumService albumService)
        {
            _photoService = photoService;
            this.orderPhotoService = orderPhotoService;
            this._photoService = photoService;
            this._albumService = albumService;
        }


        public IActionResult OnGet(int albumid, int orderid)
        {
            OrderId = orderid; // Bruges i HTML for at tjekke om værdig er større end 0
            AlbumId = albumid; // bruges i HTML for at route albumid med for at bruge den når man skal tilbage til siden.
            Photos = _photoService.GetAllPhotos()
               .Where(p => p.AlbumId.Equals(albumid)).ToList();
            OrderPhotos = orderPhotoService.GetAllPhotos().Where(o => o.OrderId.Equals(orderid)).ToList();
            List<int> tep = new List<int>();
            OrderPhotos.ForEach(p => { tep.Add(p.Quantity);});
            CartQuantity = tep.Sum();
            return Page();
        }
        /// <summary>
        /// Metoden skal håndtere at kunden kan oprette en til mange orderphoto til forskellige type af størrelse.
        /// </summary>
        /// <param name="id">PhotoId</param>
        /// <returns>til samlede OrderPhoto</returns>
        public async Task<IActionResult> OnPost(int id, int albumid, int orderId)
        {
            OrderPhoto.Size = Size;

            if (!Size.IsNullOrEmpty())
            {
                OrderPhoto.Size = Size;
            }
            else
            {
                Message = alertMessage;
                Photos = _photoService.GetAllPhotos()
                    .Where(p => p.AlbumId.Equals(albumid)).ToList();
                return Page();

            }
            OrderPhoto.OrderId = orderId;
            OrderPhoto.Price = orderPhotoService.CalculatePrice(OrderPhoto.Size, OrderPhoto.Quantity);
            OrderPhoto.PhotoId = _photoService.GetPhotoById(id).Result.Id;
            await orderPhotoService.AddPhotoToOrder(OrderPhoto);
            //Photos = _photoService.GetAllPhotos()
            //    .Where(p => p.AlbumId.Equals(albumid)).ToList();

            OrderPhotos = orderPhotoService.GetAllPhotos().Where(o => o.OrderId.Equals(orderId)).ToList();
            return RedirectToPage("/PhotoPages/GetPhotos", new { albumid, orderId });
        }
    

    }
}
