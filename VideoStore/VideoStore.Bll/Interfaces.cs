using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoStore.Bll
{
    public interface IMovieRentals
    {
        void AddRental(string movieTitle, string socialSecurityNumber);
        void RemoveRental(string movieTitle, string socialSecurityNumber);
        List<MovieRental> GetRentalsFor(string socialSecurityNumber);

    }

    public interface IVideoStore
    {
        void RegisterCustomer(string name, string socialSecurityNumber);
        void AddMovie(Movie movie);
        void RentMovie(string movieTitle, string socialSecurityNumber);
        List<Customer> GetCustomers();
        void ReturnMovie(string movieTitle, string socialSecurityNumber);
    }

    public interface IDateTime
    {
        DateTime Now();
    }
}
