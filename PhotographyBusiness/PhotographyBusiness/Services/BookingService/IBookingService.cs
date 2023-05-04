using PhotographyBusiness.Models;

namespace PhotographyBusiness.Services.BookingService
{
    public interface IBookingService
    {

        public List<Booking> GetAllBookings();
        public Task<Booking> GetBookingById(int id);
        public Task<List<Booking>> GetBookingsByUserId(int userId);
        public Task<Booking> DeleteBooking(int id);
        public Task<Booking> CreateBooking(Booking booking);
        public Task<Booking> UpdateBooking(Booking booking);

    }
}
