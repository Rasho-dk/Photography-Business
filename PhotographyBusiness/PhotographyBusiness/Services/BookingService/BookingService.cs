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
            //Bookings = _genericDbService.GetObjectsAsync().Result.ToList();
            Bookings = MockBookings.GetAllMockBookings();
        }


        public List<Booking> GetAllBookings()
        {
            return Bookings;
        }
        public List<Booking> GetAllBookingRequests()
        {
            List<Booking> bookingRequests = new List<Booking>();
            foreach(Booking booking in Bookings)
            {
                if(booking.IsAccepted == false)
                {
                    bookingRequests.Add(booking);
                }
            }
            return bookingRequests;
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
            IEnumerable<Booking> bookings = from booking in Bookings
                                     where booking.UserId == userId
                                     select booking;

            return bookings.ToList();
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
        public Task AcceptBooking(int id)
        {
            foreach(Booking booking in Bookings)
            {
                if(booking.BookingId == id)
                {
                    booking.IsAccepted = true;
                    return _genericDbService.SaveObjects(Bookings);
                }
            }
            return null;
        }

        public async Task<List<Booking>> GetAllBookingsThisMonth()
        {
            return GetAllBookings().Where(b => b.Date >= DateTime.Now.AddDays(-30) && b.IsAccepted == true).ToList();
        }

        public async Task<List<Booking>> GetUpcomingBookings()
        {
            return GetAllBookings().Where(b => b.IsAccepted == true && b.Date > DateTime.Now).ToList();
        }

        public async Task<List<Booking>> GetMostRecentRequests()
        {
            return GetAllBookings().Where(b => b.IsAccepted == false).OrderBy(b => b.DateCreated).Take(5).ToList();
        }
    }
}
