using System;
using System.Collections.Generic;

namespace VideoStore.Gui
{
    public class Rentals
    {
        public void AddRental(string title, string ssn)
        {
            throw new NotImplementedException();
        }

        public DateTime ReturnDate { get; set; }
        public string MovieTitle { get; set; }

        public List<Rentals> GetRentalsFor(string ssn)
        {
            throw new NotImplementedException();
        }
    }
}