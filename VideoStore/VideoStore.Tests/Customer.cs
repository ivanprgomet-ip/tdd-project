using System.Collections.Generic;

namespace VideoStore.Tests
{
    public class Customer
    {
        public string Name { get; set; }
        public string SSN { get; set; }
        public List<Rental> Rentals { get; set; }
    }
}