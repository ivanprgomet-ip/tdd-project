using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace VideoStore.Gui
{
    public class VideoStore: IVideoStore
    {
        private IMovieRentals rentals;
        public List<Movie> movies { get; set; }
        public List<Customer> customers { get; set; }
        

        public VideoStore(IMovieRentals rentals)
        {
            this.rentals = rentals;
            customers=new List<Customer>();
            movies = new List<Movie>();
        }
        public void AddMovie(Movie movie)
        {
            if (movie.Title == "")
                throw new MovieException();
            if (movies.Where(m => m.Title == movie.Title).Count() < 3)
            {
                movies.Add(movie);
            }
            else
            {
                throw new MovieException();
            }
        }

        public void RegisterCustomer(string name, string ssn)
        {
            if (!ValidSSN(ssn))
            {
                throw new SSNFormatException();
            }
            if (customers.Any(c => c.SocialSecurityNumber == ssn))
            {
                throw new CantAddCustomerTwiceException();
            }
            else
            {
            customers.Add(new Customer{Name = name,SocialSecurityNumber = ssn});
            }
        }

        public void RentMovie(string movieTitle, string socialSecurityNumber)
        {
            if (!movies.Contains(new Movie(movieTitle)))
            {
                throw new MovieException();
            }
            if (!customers.Contains(new Customer { SocialSecurityNumber = socialSecurityNumber }))
            {
                throw new CustomerNotRegisteredException();
            }
            
            rentals.AddRental(movieTitle,socialSecurityNumber);
        }

        public void ReturnMovie(string movieTitle, string socialSecurityNumber)
        {
            if (!rentals.GetRentalsFor(socialSecurityNumber).Any(x => x.movieTitle == movieTitle))
            {
                throw new MovieException();
            }
            rentals.RemoveRental(movieTitle,socialSecurityNumber);
        }

        public List<Customer> GetCustomers()
        {
            return customers;
        }

        public bool ValidSSN(string ssn)
        {
            var ssnRegex=@"^\d{4}-\d{2}-\d{2}$";
            if (Regex.IsMatch(ssn, ssnRegex))
            {
                return true;
            }
            return false;
        }
    }
}
