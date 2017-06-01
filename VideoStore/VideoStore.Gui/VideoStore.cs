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

        public VideoStore(IMovieRentals rentals)
        {
            this.rentals = rentals;
        }
        public void AddMovie(Movie sutMovie)
        {
            throw new NotImplementedException();
        }

        public void RegisterCustomer(string name, string ssn)
        {
            throw new NotImplementedException();
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
