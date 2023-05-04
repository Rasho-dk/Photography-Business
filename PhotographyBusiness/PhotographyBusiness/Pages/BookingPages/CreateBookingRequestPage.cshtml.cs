using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PhotographyBusiness.Models;

namespace PhotographyBusiness.Pages.BookingPages
{
    public class CreateBookingRequestPageModel : PageModel
    {
        [BindProperty] public string Name { get; set; }
        [BindProperty] public string Email { get; set; }
        [BindProperty] public string Phone { get; set; }
        [BindProperty] public string City { get; set; }
        [BindProperty] public string ZipCode { get; set; }
        [BindProperty] public string StreetName { get; set; }
        public Booking Booking { get; set; }

        public void OnGet()
        {
        }
    }
}
