using PhotographyBusiness.Models;
using PhotographyBusiness.MockData;
using PhotographyBusiness.EFDbContext;
using Microsoft.EntityFrameworkCore;

namespace PhotographyBusiness.Services.BookingService
{
    public class BookingService : IBookingService
    {

        private GenericDbService<Booking> _genericDbService;

        public List<Booking> Bookings { get; set; }

        public BookingService(GenericDbService<Booking> genericDbService)
        {
            _genericDbService = genericDbService;
            //Bookings = GetAllBookingsAsync().Result;
            Bookings = MockBookings.GetAllMockBookings();
        }


        internal async Task<List<Booking>> GetAllBookingsAsync()
        {
            using (var context = new ObjectDbContext()) // Silas: vi skal også have useren med, når vi kalder på bookingen
            {
                return await context.Bookings.Include(b => b.User).AsNoTracking().ToListAsync();
            }
        }

        public List<Booking> GetAllBookings()
        {
            
            return Bookings;
        }

        public Booking GetBookingById(int id)
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
		public Booking GetBookingById_User(int id)
		{
            foreach (Booking booking in Bookings)
            {
                if (id == booking.User.UserId)
                {
                    return booking;
                }
            }
            return null;
        }

		public List<Booking> GetBookingsByUserId(int userId)
        {
            IEnumerable<Booking> bookings = from booking in Bookings
                                     where booking.User.UserId == userId
                                     select booking;

            return bookings.ToList();
        }

        public async Task CreateBookingAsync(Booking booking)
        {
            Bookings.Add(booking);
            await _genericDbService.AddObjectAsync(booking);
        }

        public Task DeleteBooking(int id)
        {
            Bookings.Remove(GetBookingById(id));
            return _genericDbService.DeleteObjectAsync(_genericDbService.GetObjectByIdAsync(id).Result);
        }

        public Task UpdateBooking(Booking booking)
        {
            return _genericDbService.UpdateObjectAsync(booking);
        }

        public List<Booking> GetAllBookingsThisMonth()
        {
            return GetAllBookings().Where(b => b.Date >= DateTime.Now.AddDays(-30) && b.IsAccepted == true).ToList();
        }

        public List<Booking> GetUpcomingBookings()
        {
            return GetAllBookings().Where(b => b.IsAccepted == true && b.Date > DateTime.Now).ToList();
        }

        public List<Booking> GetMostRecentRequests()
        {
            return GetAllBookings().Where(b => b.IsAccepted == false).OrderBy(b => b.DateCreated).Take(5).ToList();
        }

        public List<Booking> GetAllLBookingsRequests()
        {
            return GetAllBookings().Where(b => b.IsAccepted == false).ToList();
        }
    }
}
