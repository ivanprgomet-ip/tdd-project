using System.Collections.Generic;

namespace VideoStore.Gui
{
    public class Customer
    {
        public string Name { get; set; }
        public string SSN { get; set; }
        public List<MovieRentals> Rentals { get; set; }
    }
}