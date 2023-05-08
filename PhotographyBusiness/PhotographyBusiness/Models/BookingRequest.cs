namespace PhotographyBusiness.Models
{
    public class BookingRequest
    {
        public int BookingRequestId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string City { get; set; }
        public string ZipCode { get; set; }
        public string Street { get; set; }
        public string CustomerNote { get; set; }
        public string Category { get; set; }
        
        /// <summary>
        /// The full constructor
        /// </summary>
        /// <param name="bookingRequestId">The id for the request</param>
        /// <param name="name">The name of the customer</param>
        /// <param name="email">The Customers email</param>
        /// <param name="phone">The Customers phonenumber</param>
        /// <param name="city">The city for the event</param>
        /// <param name="zipCode">Zipcode of the place for the event</param>
        /// <param name="street">The name and maybe number of the street for the event</param>
        /// <param name="customerNote">A note if the customer has any extra notes</param>
        /// <param name="category">Category of the event i.e. wedding or birthday</param>
        public BookingRequest(int bookingRequestId, string name, string email, string phone, string city, string zipCode, string street, string customerNote, string category)
        {
            BookingRequestId = bookingRequestId;
            Name = name;
            Email = email;
            Phone = phone;
            City = city;
            ZipCode = zipCode;
            Street = street;
            CustomerNote = customerNote;
            Category = category;
        }

        /// <summary>
        /// Empty constructor for DB
        /// </summary>
        public BookingRequest()
        {
            
        }
    }

}
