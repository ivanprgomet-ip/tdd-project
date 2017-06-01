using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoStore.Gui
{
    public class VideoStore
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
        public void AddMovie(Movie sutMovie)
        {
            if (sutMovie.Title == "")
                throw new MovieTitleCannotBeEmptyException();
            else
                movies.Add(sutMovie);
        }

        public void RegisterCustomer(string name, string ssn)
        {
            if (customers.Any(c => c.SocialSecurityNumber == ssn))
            {
                throw new CantAddCustomerTwiceException();
            }
            else
            {
            customers.Add(new Customer{Name = name,SocialSecurityNumber = ssn,Rentals = new List<MovieRental>()});
            }
        }

        public void RentMovie(Movie sutMovie)
        {
            throw new NotImplementedException();
        }

        public void RentMovie(string title, string sSN)
        {
            throw new NotImplementedException();
        }

        public Movie ReturnMovie(string sutMovieTitle, string sutCustomerSsn)
        {
            throw new NotImplementedException();
        }
    }
}
