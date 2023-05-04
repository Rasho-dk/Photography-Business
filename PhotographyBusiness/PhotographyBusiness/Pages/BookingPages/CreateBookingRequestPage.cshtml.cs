using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
<<<<<<< HEAD
using PhotographyBusiness.Models;
=======
>>>>>>> 649b07b27c427f5600eccbebf68f23a0ef347265

namespace PhotographyBusiness.Pages.BookingPages
{
    public class CreateBookingRequestPageModel : PageModel
    {
<<<<<<< HEAD
        [BindProperty] public string Name { get; set; }
        [BindProperty] public string Email { get; set; }
        [BindProperty] public string Phone { get; set; }
        [BindProperty] public string City { get; set; }
        [BindProperty] public string ZipCode { get; set; }
        [BindProperty] public string StreetName { get; set; }
        public Booking Booking { get; set; }

=======
>>>>>>> 649b07b27c427f5600eccbebf68f23a0ef347265
        public void OnGet()
        {
        }
    }
}
