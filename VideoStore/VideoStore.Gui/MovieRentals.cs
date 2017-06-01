using System;
using System.Collections.Generic;
using System.Linq;

namespace VideoStore.Gui
{
    public class MovieRentals:IMovieRentals
    {
        private List<MovieRental> rentals = new List<MovieRental>();

        public void AddRental(string title, string ssn)
        {

            if (rentals.Where(r => r.socialSecurityNumber == ssn).ToList().Count == 3)
                throw new MaximumThreeMoviesToRentalException();
            if (rentals.Contains(rentals.Where(r => r.movieTitle == title).FirstOrDefault()))
                throw new CantPossessTwoCopiesOfSameVideoException();
            else
            {
                MovieRental rental = new MovieRental(title, ssn, DateTime.Now.AddDays(3));
                rentals.Add(rental);
            }
        }

        public DateTime ReturnDate { get; set; }
        public string MovieTitle { get; set; }

        public List<MovieRental> GetRentalsFor(string ssn)
        {
            return rentals;
        }

        public void RemoveRental(string movieTitle, string socialSecurityNumber)
        {
            throw new NotImplementedException();
        }
    }
}