using System;
using System.Collections.Generic;

namespace VideoStore.Gui
{
    public class MovieRentals
    {
        private List<MovieRental> rentals = new List<MovieRental>();

        public void AddRental(string title, string ssn)
        {
            MovieRental rental = new MovieRental(title, ssn, DateTime.Now.AddDays(3));

            rentals.Add(rental);
        }

        public DateTime ReturnDate { get; set; }
        public string MovieTitle { get; set; }

        public List<MovieRental> GetRentalsFor(string ssn)
        {
            return rentals;
        }
    }
}