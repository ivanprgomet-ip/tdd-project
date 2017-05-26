using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoStoreBusinessLogic.Interfaces
{
    public interface IRentals
    {
        void AddRental(string movieTitle, string socialSecurityNumber);
        void RemoveRental(string movieTitle, string socialSecurityNumber);
        List<Rental> GetRentalsFor(string socialSecurityNumber);

    }
}
