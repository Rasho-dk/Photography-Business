﻿using PhotographyBusiness.Models;

namespace PhotographyBusiness.MockData
{
    public class MockBookings
    {


        private static List<Booking> Bookings = new List<Booking>()
        {
            new Booking("Marriage", 300, "Please give me photos :3", "Yaaaah :3", new DateTime (2023,03,12), new DateTime (2023,03,13), true, "Mock Address", new User("mock@email.com", "password", "Mock User1", "12345678"), 1),
            new Booking("Family", 200, "I want me some photos motherfucker", "aight bet", new DateTime(2023,04,15), new DateTime (2023,04,18), true, "Mock Address", new User("mock@email.com", "password", "Mock User2", "12345678"), 1),
            new Booking("Portrait", 300, "Give me photos", "Ok", new DateTime(2023,06,2), new DateTime (2023,04,18), true, "Mock Address", new User("mock@email.com", "password", "Mock User3", "12345678"), 1),
            new Booking("Portrait", 300, "Give me photos", "Ok", new DateTime(2023,06,15), new DateTime (2023,04,18), true, "Mock Address", new User("mock@email.com", "password", "Mock User4", "12345678"), 1),
            new Booking("Portrait", 300, "Give me photos", "Ok", new DateTime(2023,06,27), new DateTime (2023,04,18), true, "Mock Address", new User("mock@email.com", "password", "Mock User5", "12345678"), 1),
            // Arun: understående er Bookings requests
            new Booking("Marriage", "pls take pics", new DateTime (2023,05,01), "Mock Address", new User("mock@email.com", "password", "Mock User6", "12345678")),
            new Booking("Marriage", "pls again", new DateTime (2023,05,03), "Mock Address", new User("mock@email.com", "password", "Mock User7", "12345678"))


        };

    public static List<Booking> GetAllMockBookings()
    {
        return Bookings;
    }

    }
}

