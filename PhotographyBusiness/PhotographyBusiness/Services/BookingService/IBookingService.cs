using PhotographyBusiness.Models;

namespace PhotographyBusiness.Services.BookingService
{
    public interface IBookingService
    {

        public List<Booking> GetAllBookings();
        public Task<List<Booking>> GetAllBookingsAsync();
        public Booking GetBookingById(int id);
        public List<Booking> GetBookingsByUserId(int userId);
        public Task DeleteBooking(int id);
        public Task CreateBookingAsync(Booking booking);
        public Task UpdateBooking(Booking booking);
        public Task ConfirmBooking(Booking booking);
        public List<Booking> GetAllBookingsThisMonth();
        public List<Booking> GetUpcomingBookings();
        public List<Booking> GetMostRecentRequests();
        public List<Booking> GetAllBookingsRequests();
        public Task<List<Booking>> FilterBookingsByDate(DateTime startdate, DateTime endDate);
        public Task<List<Booking>> FilterBookingsByNameOrEmail(string searchinput);
        public Task<List<Booking>> FilterBookingsByCategory(string Category);
        public Task<List<Booking>> SortBookingByCategory();
        public Task<List<Booking>> SortBookingByDate();
        public Task<List<Booking>> SortBookingByName();
        public Task<List<Booking>> SortBookingByEmail();
        Booking GetBookingById_User1(int id, int id2);

    }
}
