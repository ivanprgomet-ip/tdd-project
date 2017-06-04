using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VideoStore.Bll;

namespace VideoStore.Tests
{
    [TestFixture]
    [Category("TESTRentals")]
    public class MovieRentalsTests
    {
        //Behöver inte ha Video och Customer här.
        private MovieRentals sut { get; set; }
        private Movie testVideo { get; set; }
        private Customer testCustomer { get; set; }
        private IDateTime dateTime;

        [SetUp]
        public void Setup()
        {
            dateTime = Substitute.For<IDateTime>();
            sut = new MovieRentals(dateTime);
            testCustomer = new Customer{ Name = "Tess", SocialSecurityNumber = "123"};
            testVideo=new Movie{Title = "Titanic"};
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
            MovieRental r = new MovieRental();
            r.movieTitle = "Die HArd";
            r.dueDate = DateTime.Now.AddDays(3);
            testCustomer.SocialSecurityNumber = "123";

            sut.AddRental(r.movieTitle, testCustomer.SocialSecurityNumber);

            Assert.AreEqual(DateTime.Now.AddDays(3).Date, r.dueDate.Date);


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
        public void CannotRentTwoCopiesOfTheSameMovie()
        {
            Movie testMovie1 = new Movie()
            {
                Title = "dirty dancing",
            };

            sut.AddRental(testMovie1.Title, testCustomer.SocialSecurityNumber);

            Assert.Throws<CantPossessTwoCopiesOfSameVideoException>(()
                => sut.AddRental(testMovie1.Title, testCustomer.SocialSecurityNumber));
        }
        [Test]
        public void CannotRentMovieIfCustomerHasAMovieWithExpiredDueDate()
        {
            Movie testMovie = new Movie("die hard");

            var fakeDate = new DateTime(2017, 05, 12);

            dateTime.Now().Returns(fakeDate); // the datetime now will be the fakedate

            sut.AddRental(testVideo.Title, testCustomer.SocialSecurityNumber);

            dateTime.Now().Returns(fakeDate.AddDays(10));

            Assert.Throws<MovieWithExpiredDateFoundException>(()
                => sut.AddRental(testMovie.Title,testCustomer.SocialSecurityNumber));
        }
    }
}
