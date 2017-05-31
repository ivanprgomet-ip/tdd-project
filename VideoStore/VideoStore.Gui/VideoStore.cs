using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoStore.Gui
{
    public class VideoStore
    {
        private IRentals rentals;

        public VideoStore(IRentals rentals)
        {
            this.rentals = rentals;
        }
        public void AddMovie(Video sutMovie)
        {
            throw new NotImplementedException();
        }

        public void RegisterCustomer(string name, string ssn)
        {
            throw new NotImplementedException();
        }

        public void RentMovie(Video sutMovie)
        {
            throw new NotImplementedException();
        }

        public void RentMovie(string title, string sSN)
        {
            throw new NotImplementedException();
        }

        public Video ReturnMovie(string sutMovieTitle, string sutCustomerSsn)
        {
            throw new NotImplementedException();
        }
    }
}
