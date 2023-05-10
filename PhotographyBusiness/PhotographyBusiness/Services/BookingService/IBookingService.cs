using PhotographyBusiness.Models;

namespace PhotographyBusiness.Services.BookingService
{
    public interface IBookingService
    {

        public List<Booking> GetAllBookings();
        public Booking GetBookingById(int id);
        public List<Booking> GetBookingsByUserId(int userId);
        public Task DeleteBooking(int id);
        public Task CreateBookingAsync(Booking booking);
        public Task UpdateBooking(Booking booking);
        public List<Booking> GetAllBookingsThisMonth();
        public List<Booking> GetUpcomingBookings();
        public List<Booking> GetMostRecentRequests();
        public List<Booking> GetAllLBookingsRequests();
        Booking GetBookingById_User(int id);


	}
}
