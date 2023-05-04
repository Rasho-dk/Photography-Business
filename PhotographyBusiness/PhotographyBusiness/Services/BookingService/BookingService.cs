using PhotographyBusiness.Models;
using PhotographyBusiness.MockData;

namespace PhotographyBusiness.Services.BookingService
{
    public class BookingService : IBookingService
    {

        private GenericDbService<Booking> _genericDbService;

        public List<Booking> Bookings { get; set; }

        public BookingService(GenericDbService<Booking> genericDbService)
        {
            _genericDbService = genericDbService;
            //Bookings = _genericDbService.GetObjectsAsync<Booking>().Result.ToList();
            Bookings = new List<Booking>();
        }


        public List<Booking> GetAllBookings()
        {
            return Bookings;
        }

        public async Task<Booking> GetBookingById(int id)
        {
            foreach(Booking booking in Bookings)
            {
                if(id == booking.BookingId)
                {
                    return booking;
                }
            }
            return null;
        }

        public List<Booking> GetBookingsByUserId(int userId)
        {
            //List<Booking> bookings = from booking in Bookings 
            //                         where booking.UserId == userId 
            //                         select booking;

            //return bookings;
            return null;
        }

        public Task CreateBooking(Booking booking)
        {
            return _genericDbService.AddObjectAsync(booking);
        }

        public Task DeleteBooking(int id)
        {
            return _genericDbService.DeleteObjectAsync(_genericDbService.GetObjectByIdAsync(id).Result);
        }

        public Task UpdateBooking(Booking booking)
        {
            return _genericDbService.UpdateObjectAsync(booking);
        }
    }
}
