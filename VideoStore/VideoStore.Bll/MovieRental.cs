using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoStore.Bll
{
    public class MovieRental
    {
        public string movieTitle;
        public string socialSecurityNumber;
        public DateTime dueDate;

        public MovieRental()
        {

        }
        public MovieRental(string title, string ssn, DateTime due)
        {
            dueDate = due;
            movieTitle = title;
            socialSecurityNumber = ssn;
        }
    }
}
