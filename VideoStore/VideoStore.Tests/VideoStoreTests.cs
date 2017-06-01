using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using NSubstitute;
using VideoStore.Gui;

namespace VideoStore.Tests
{
    [TestFixture]
    public class VideoStoreTests
    {
        private Gui.VideoStore sut { get; set; }
        private Movie testMovie { get; set; }
        private Customer testCustomer { get; set; }
        private MovieRentals rentalsMock { get; set; }


        [SetUp]
        public void Setup()
        {
            rentalsMock = new MovieRentals();
            sut = new Gui.VideoStore(rentalsMock);
            testMovie = new Movie();
            testCustomer = new Customer() { Name = "Tess", SocialSecurityNumber = "123", Rentals = new List<MovieRental>() };
        }
        [Test]
        public void CannotAddEmptyMovieTitle()
        {
            testMovie.Title = "";

            Assert.Throws<MovieTitleCannotBeEmptyException>(() =>
                sut.AddMovie(testMovie));
        }
        [Test]
        public void CannotAddFourthCopyOfSameMovie()
        {
            sut.AddMovie(testMovie);
            sut.AddMovie(testMovie);
            sut.AddMovie(testMovie);

            Assert.Throws<MaximumThreeMoviesException>(() =>
                sut.AddMovie(testMovie));
        }
        [Test]
        public void CannotAddSameCustomerTwice()
        {
            testCustomer.Name = "therese";
            testCustomer.SocialSecurityNumber = "123";

            sut.RegisterCustomer(testCustomer.Name, testCustomer.SocialSecurityNumber);

            Assert.Throws<CantAddCustomerTwiceException>(()
                => sut.RegisterCustomer(testCustomer.Name, testCustomer.SocialSecurityNumber));
        }
        [Test]
        public void MustFollowSSNFormatWhenRegisteringNewCustomer()
        {
            testCustomer.Name = "Ivan";
            testCustomer.SocialSecurityNumber = "1234-2-2";

            Assert.Throws<SSNFormatException>(() => sut.RegisterCustomer(testCustomer.Name, testCustomer.SocialSecurityNumber));
        }

        [Test]
        public void CannotRentNonExistentMovie()
        {
            testMovie.Title = "Die Hard";
            Assert.Throws<MovieDoesntExistException>(()
                => sut.RentMovie("non existent movie title","123"));

        }
        [Test]
        public void CannotRentMovieAsAnUnregisteredCustomer()
        {
            testMovie.Title = "Dirty dancing";
            testCustomer.SocialSecurityNumber = "843";

            var e = Assert.Throws<CustomerNotRegisteredException>(()
                => sut.RentMovie(testMovie.Title, testCustomer.SocialSecurityNumber));

            StringAssert.Contains("The customer is not registered", e.Message);
        }
    }
    
}
