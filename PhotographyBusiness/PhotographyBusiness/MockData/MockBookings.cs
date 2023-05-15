using PhotographyBusiness.Models;

namespace PhotographyBusiness.MockData
{
    public class MockBookings
    {


        private static List<Booking> Bookings = new List<Booking>()
        {
            new Booking("Marriage", 100, "Please give me photos :3", "Yaaaah :3", new DateTime (2023,03,12,14,15,00), new DateTime (2023,03,13), true, "Mrs Rasho 71 Cherry Court SOUTHAMPTON SO53 5PD UK", new User(4,"RashoRasho@hotmail.com", "123","Rasho Rash", "Rasho123"), 1) {BookingId = 1},
            new Booking("Family", 200, "I need 200 photos during 2 hours and if it's possible to film two so I will be great full", "aight bet", new DateTime(2023,04,15,16,30,00), new DateTime (2023,04,18), true, "Mock Address", new User(3,"Silas@outlook.com", "tyler1", "Silas Silas", "42791451"), 1){BookingId = 2},
            new Booking("Portrait", 300, "I need 200 photos during 2 hours and if it's possible to film two so I will be great full", "Ok", new DateTime(2023,06,2,20,00,00), new DateTime (2024,04,18), true, "Mrs Rasho 150 Cherry Court SOUTHAMPTON SO53 5PD UK", new User(4,"RashoRasho@hotmail.com", "123","Rasho Rash", "Rash321"), 1){BookingId = 3},
            //new Booking("Portrait", 400, "Give me photos", "Ok", new DateTime(2023,06,15), new DateTime (2023,04,18), true, "Mock Address", new User(4,"mock@email.com", "password", "Mock User", "12345678"), 1){BookingId = 4},
            //new Booking("Portrait", 500, "Give me photos", "Ok", new DateTime(2023,06,27), new DateTime (2023,04,18), true, "Mock Addressasdasdsaaaaaaaaadasdasdasdasdasdasdasdasdsadasdasdasdasdasd", new User(5, "mock@email.com", "password", "Mock User", "12345678"), 1),
            // Arun: understående er Bookings requests
            new Booking("Party", "pls take pics", "Mrs Bo 300 Cherry Court SOUTHAMPTON SO53 5PD UK", new User("Bo@email.com", "123", "Bo Bo", "12345678"), new DateTime(2023, 10, 10,12,30,00)) { BookingId = 123},
            new Booking("Marriage", "pls again", "Mrs Peter 121 Cherry Court SOUTHAMPTON SO53 5PD UK", new User("Peter@email.com", "123", "Peter Peter", "12345678"), new DateTime(2023, 10, 10,22,30,00)) { BookingId = 321}


        };

        public static List<Booking> GetAllMockBookings()
        {
            return Bookings;
        }

    }
}

