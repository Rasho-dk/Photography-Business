using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PhotographyBusiness.Models;
using PhotographyBusiness.Services.AlbumService;
using PhotographyBusiness.Services.PhotoService;

namespace PhotographyBusiness.Pages.PhotoPages
{
    public class AddPhotoToAlbumModel : PageModel
    {
        private IPhotoService photoService;
        private IAlbumService albumService;
        //Den bruges til at f� oplysninger fx at finde hvor file ligger hen ligesom en sti til finde file
        private IWebHostEnvironment webHostEnvironment;
        public Album Album { get; private set; }

        [BindProperty]
        public Photo Photo { get; set; }
        [BindProperty]
        public List<IFormFile> Imgs { get; set; }
        public AddPhotoToAlbumModel(IPhotoService photoService, IAlbumService albumService, IWebHostEnvironment webHostEnvironment)
        {
            this.photoService = photoService;
            this.albumService = albumService;
            this.webHostEnvironment = webHostEnvironment;
        }

        public IActionResult OnGet(int albumid)
        {
            Album = albumService.GetAlbumByIdAsync(albumid);
            return Page();
        }
        public async Task<IActionResult> OnPostAsync(int albumid)
        {
            //if (!ModelState.IsValid)
            //{
            //    return Page();
            //}
            //List<IFormFile> images = Photo.ImageFiles;
            if (Imgs != null && Imgs.Count > 0)
            {
                foreach (var imagefile in Imgs)
                {
                    //if (Photo.ImageFile != null && Photo.ImageFile.Length > 0)

                    // Path.Combine bruges at kombinere stier og filenavn til et format og oprette korrkte stier til filer eller mapper.
                    string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "mediaUpload");
                    //Den giver et random navn til Path + navn p� 
                    //string uniqueFileName = Path.GetRandomFileName() + "-" + Photo.ImageFile.FileName;
                    string uniqueFileName = Path.GetRandomFileName() + "-" + Path.GetFileName(imagefile.FileName);
                    //Fx den vil vise \path\to\wwwroot\uniqueFileName.jpg
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    //FileStram-objektet bliver oprettet ved hj�pl af filepath og FileMode.Create)
                    //FileMode Create: betyder at hvis filen eksistere, bliver den slettet og en ny fil bliver det oprettet.
                    //
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        //await Photo.ImageFile.CopyToAsync(fileStream);
                        await imagefile.CopyToAsync(fileStream);
                    }
                    Photo photo = new Photo();
                    photo.AlbumId = albumService.GetAlbumByIdAsync(albumid).Id;
                    photo.ImageFile = imagefile;
                    photo.FilePath = uniqueFileName;                
                    _ = photoService.AddPhotoAsync(photo);

                }


                return RedirectToPage("/AlbumPages/Index");

            }
            return Page();

        }
    }
}