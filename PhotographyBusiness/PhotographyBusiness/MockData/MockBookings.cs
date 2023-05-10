using PhotographyBusiness.Models;

namespace PhotographyBusiness.MockData
{
    public class MockBookings
    {


        private static List<Booking> Bookings = new List<Booking>()
        {
            new Booking("Marriage", 300, "Please give me photos :3", "Yaaaah :3", new DateTime (2023,03,12), new DateTime (2023,03,13), true, "Mock Address", new User("mock@email.com", "password", "Mock User", "12345678"), 1),
            new Booking("Family", 200, "I want me some photos motherfucker", "aight bet", new DateTime(2023,04,15), new DateTime (2023,04,18), true, "Mock Address", new User("mock@email.com", "password", "Mock User", "12345678"), 1),
            new Booking("Portrait", 300, "Give me photos", "Ok", new DateTime(2023,06,2), new DateTime (2023,04,18), true, "Mock Address", new User("mock@email.com", "password", "Mock User", "12345678"), 1),
            new Booking("Portrait", 300, "Give me photos", "Ok", new DateTime(2023,06,15), new DateTime (2023,04,18), true, "Mock Address", new User("mock@email.com", "password", "Mock User", "12345678"), 1),
            new Booking("Portrait", 300, "Give me photos", "Ok", new DateTime(2023,06,27), new DateTime (2023,04,18), true, "Mock Address", new User("mock@email.com", "password", "Mock User", "12345678"), 1),
            // Arun: understående er Bookings requests
            new Booking("Marriage", "pls take pics", "Mock Address", new User("mock@email.com", "password", "Mock User 1", "12345678"), new DateTime(2023, 10, 10)),
            new Booking("Marriage", "pls again", "Mock Address", new User("mock@email.com", "password", "Mock User 2", "12345678"), new DateTime(2023, 10, 10))


        };

    public static List<Booking> GetAllMockBookings()
    {
        return Bookings;
    }

    }
}

