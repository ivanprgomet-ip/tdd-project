using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoStore.Gui;

namespace VideoStore.Tests
{
    [TestFixture]
    public class MovieRentalsTests
    {
        //Behöver inte ha Video och Customer här.
        private MovieRentals sut { get; set; }
        private Movie testVideo { get; set; }
        private Customer testCustomer { get; set; }

        [SetUp]
        public void Setup()
        {
            sut = new MovieRentals();
            testCustomer = new Customer() { Name = "Tess", SocialSecurityNumber = "123", Rentals = new List<MovieRentals>() };
        }
        [Test]
        public void CanAddRental()
        {
            Movie testMovie = new Movie();
            testMovie.Title = "Star wars";

            sut.AddRental(testMovie.Title, testCustomer.SocialSecurityNumber);

            List<MovieRental> rents = sut.GetRentalsFor(testCustomer.SocialSecurityNumber);

            Assert.AreEqual(1, rents.Count);
        }
        [Test]
        public void MovieRentalGetsThreeDayLaterDueDate()
        {
            MovieRentals r = new MovieRentals();
            r.MovieTitle = "Die HArd";
            r.ReturnDate = DateTime.Now.AddDays(3);
            testCustomer.SocialSecurityNumber = "123";

            sut.AddRental(r.MovieTitle, testCustomer.SocialSecurityNumber);

            Assert.AreEqual(DateTime.Now.AddDays(3).Date, r.ReturnDate.Date);


        }
        [Test]
        public void CanGetMovieRentalBySocialSecurityNumber()
        {
            Movie testMovie = new Movie();
            testMovie.Title = "die hard";

            sut.AddRental(testMovie.Title, testCustomer.SocialSecurityNumber);

            List<MovieRental> rentals = sut.GetRentalsFor(testCustomer.SocialSecurityNumber);

            Assert.AreEqual(1, rentals.Count);

            StringAssert.Contains("die hard", rentals[0].movieTitle);
        }

        [Test]
        public void CanRentMoreThanOneMovie()
        {
            Movie testMovie1 = new Movie()
            {
                Title = "dirty dancing",
            };
            Movie testMovie2 = new Movie()
            {
                Title = "die hard",
            };

            sut.AddRental(testMovie1.Title, testCustomer.SocialSecurityNumber);
            sut.AddRental(testMovie2.Title, testCustomer.SocialSecurityNumber);

            List<MovieRental> rentals = sut.GetRentalsFor(testCustomer.SocialSecurityNumber);

            Assert.AreEqual(2, rentals.Count);
        }
        [Test]
        public void CannotRentMoreThanThreeMovies()
        {
            Movie testMovie1 = new Movie()
            {
                Title = "dirty dancing",
            };
            Movie testMovie2 = new Movie()
            {
                Title = "die hard",
            };
            Movie testMovie3 = new Movie()
            {
                Title = "titanic",
            };
            Movie testMovie4 = new Movie()
            {
                Title = "fury",
            };


            sut.AddRental(testMovie1.Title, testCustomer.SocialSecurityNumber);
            sut.AddRental(testMovie2.Title, testCustomer.SocialSecurityNumber);
            sut.AddRental(testMovie3.Title, testCustomer.SocialSecurityNumber);

            Assert.Throws<MaximumThreeMoviesToRentalException>(() =>
                sut.AddRental(testMovie4.Title, testCustomer.SocialSecurityNumber));
        }
        [Test]
        public void CannotHaveTwoCopiesOfTheSameMovie()
        {
            Movie v1 = new Movie() { Title = "die hard" };
            Customer c1 = new Customer() { Name = "ivan", SocialSecurityNumber = "123", Rentals = new List<MovieRentals>() };


            sut.AddRental(v1.Title, c1.SocialSecurityNumber);

            Assert.Throws<CantPossessTwoCopiesOfSameVideoException>(()
                => sut.AddRental(v1.Title, c1.SocialSecurityNumber));

            Assert.AreEqual(1, c1.Rentals.Count);
        }
        [Test]
        public void CannotRentMovieIfCustomerHasAMovieWithExpiredDueDate()
        {

        }
    }
}
