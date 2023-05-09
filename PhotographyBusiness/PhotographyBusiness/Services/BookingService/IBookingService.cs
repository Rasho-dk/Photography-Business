using PhotographyBusiness.Models;

namespace PhotographyBusiness.Services.BookingService
{
    public interface IBookingService
    {

        public List<Booking> GetAllBookings();
        public Task<Booking> GetBookingById(int id);
        public List<Booking> GetBookingsByUserId(int userId);
        public Task DeleteBooking(int id);
        public Task CreateBooking(Booking booking);
        public Task UpdateBooking(Booking booking);
        public Task<List<Booking>> GetAllBookingsThisMonth();
        public Task<List<Booking>> GetUpcomingBookings();
        public Task<List<Booking>> GetMostRecentRequests();
        public IEnumerable<Booking> FilterBookingsByDate(DateTime date);

    }
}
