﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace VideoStore.Bll
{
    public class MovieRentals:IMovieRentals
    {
        private List<MovieRental> rentals;
        private IDateTime returnTime;
        public string MovieTitle { get; set; }

        public MovieRentals(IDateTime dateTime)
        {
            this.returnTime = dateTime;
            rentals=new List<MovieRental>();
        }

        public void AddRental(string title, string ssn)
        {
            #region checks if there are any movies that have expired due date
            List<MovieRental> expiredRentals = new List<MovieRental>();
            foreach (var r in rentals)
            {
                if (r.socialSecurityNumber == ssn && 
                    r.dueDate < returnTime.Now()) // the now() is actually the method in the IDateTime interface
                {
                    expiredRentals.Add(r);
                }
            }
            if (expiredRentals.Count > 0)
                throw new MovieWithExpiredDateFoundException();
            #endregion

            if (rentals.Where(r => r.socialSecurityNumber == ssn).ToList().Count == 3)
                throw new MaximumThreeMoviesToRentalException();
            if (rentals.Contains(rentals.Where(r => r.movieTitle == title).FirstOrDefault()))
                throw new CantPossessTwoCopiesOfSameVideoException();
            else
            {
                MovieRental rental = new MovieRental(title, ssn, returnTime.Now().AddDays(3));
                rentals.Add(rental);
            }
        }

        

        public List<MovieRental> GetRentalsFor(string ssn)
        {
            return rentals.Where(x => x.socialSecurityNumber == ssn).ToList();
        }

        public void RemoveRental(string movieTitle, string socialSecurityNumber)
        {
            rentals.Remove(rentals.Where(r => r.movieTitle == movieTitle && r.socialSecurityNumber == socialSecurityNumber).FirstOrDefault());
        }
    }
}