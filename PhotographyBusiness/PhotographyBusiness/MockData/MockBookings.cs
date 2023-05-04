using PhotographyBusiness.Models;

namespace PhotographyBusiness.MockData
{
    public class MockBookings
    {
        //public static List<Booking> BookingRequests = new List<Booking>()
        //{

        //    new Booking("Marriage", "pls take pics", new DateTime (2023,05,01), new DateTime (2023,05,03)),
        //    new Booking("Marriage", "pls again", new DateTime (2023,05,03), new DateTime (2023, 05,06))

        //};

        private static List<Booking> Bookings = new List<Booking>()
        {
            // Arun: MockBookings af accepterede bookings.
            new Booking("Marriage", 300, "Please give me photos :3", "Yaaaah :3", new DateTime (2023,03,12), new DateTime (2023,03,13), true ),
            new Booking("Family", 200, "I want me some photos motherfucker", "aight bet", new DateTime(2023,04,15), new DateTime (2023,04,18), true ),
            // Arun: Understående er Request Bookings, som PO ville kunne accept/deny. 
            new Booking("Marriage", "pls take pics", new DateTime (2023,05,01), new DateTime (2023,05,03)),
            new Booking("Marriage", "pls again", new DateTime (2023,05,03), new DateTime (2023, 05,06))

        };

        public static List<Booking> GetAllMockBookings()
        {
            return Bookings;
        }


    }
}
