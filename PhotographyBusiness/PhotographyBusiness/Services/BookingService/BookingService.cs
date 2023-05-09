using PhotographyBusiness.Models;
using PhotographyBusiness.MockData;

namespace PhotographyBusiness.Services.BookingService
{
    public class BookingService : IBookingService
    {

        private GenericDbService<Booking> _genericDbService;

        public List<Booking> Bookings { get; set; }

        public int number { get; set; }

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

        public IEnumerable<Booking> FilterBookingsByDate(DateTime date)
        {
            return from booking in Bookings
                   where booking.Date >= date
                   select booking;

        }
        public async Task<List<Booking>> FilterBookingsByName(string name)
        {
            IEnumerable<Booking> filteredBookings = from booking in Bookings 
                                                    where booking.User.Name.Contains(name)
                                                    select booking;
            return filteredBookings.ToList();
        }

        public async Task<List<Booking>> FilterBookingsByEmail(string Email)
        {
            IEnumerable<Booking> filteredBookings = from booking in Bookings
                                                    where booking.User.Email.ToLower().Contains((Email))
                                                    select booking;
            return filteredBookings.ToList();
        }

        public async Task<List<Booking>> FilterBookingsByCategory(string Category)
        {
            IEnumerable<Booking> filteredBookings = from booking in Bookings
                                                    where booking.Category.ToLower().Contains((Category))
                                                    select booking;
            return filteredBookings.ToList();
        }


    }
}
