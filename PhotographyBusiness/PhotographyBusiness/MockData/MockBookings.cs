﻿using PhotographyBusiness.Models;

namespace PhotographyBusiness.MockData
{
    public class MockBookings
    {


        private static List<Booking> Bookings = new List<Booking>()
        {
            new Booking("Marriage", 700, "Please give me photos :3", "Yaaaah :3", new DateTime (2023,03,12), new DateTime (2023,03,13), true, "Mock Address", new User("mock@email.com", "password", "Mock User", "12345678"), 1),
            new Booking("Family", 3200, "I want me some photos motherfucker", "aight bet", new DateTime(2023,04,15), new DateTime (2023,04,18), true, "Mock Address", new User("mock@email.com", "password", "Mock User", "12345678"), 1),
            new Booking("Portrait", 200, "Give me photos", "Ok", new DateTime(2023,06,2), new DateTime (2023,04,18), true, "Mock Address", new User("mock@email.com", "password", "Mock User", "12345678"), 1),
            new Booking("Portrait", 100, "Give me photos", "Ok", new DateTime(2023,06,15), new DateTime (2023,04,18), true, "Mock Address", new User("mock@email.com", "password", "Mock User", "12345678"), 1),
            new Booking("Portrait", 400, "Give me photos", "Ok", new DateTime(2023,06,27), new DateTime (2023,04,18), true, "Mock Address", new User("mock@email.com", "password", "Mock User", "12345678"), 1),
            // Arun: understående er Bookings requests
            new Booking("Marriage", "pls take pics", new DateTime (2023,05,01), "Mock Address", new User("mock@email.com", "password", "Mock User 1", "12345678")),
            new Booking("Marriage", "pls again",new DateTime (2023,05,03), "Mock Address", new User("mock@email.com", "password", "Mock User 2", "12345678"))


        };

    public static List<Booking> GetAllMockBookings()
    {
        return Bookings;
    }

    }
}

