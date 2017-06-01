using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
            testMovie = new Movie{Title = "Die Hard"};
            testCustomer = new Customer() { Name = "Tess", SocialSecurityNumber = "1991-02-23"};
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
            testCustomer.SocialSecurityNumber = "1984-01-12";

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
            sut.RegisterCustomer(testCustomer.Name,testCustomer.SocialSecurityNumber);
            Assert.Throws<MovieDoesntExistException>(()
                => sut.RentMovie("Titanic",testCustomer.SocialSecurityNumber));

            //rentalsMock.DidNotReceive().AddRental(Arg.Any<string>(), Arg.Any<string>());
        }
        [Test]
        public void CannotRentMovieAsAnUnregisteredCustomer()
        {
            sut.AddMovie(testMovie);

            Assert.Throws<CustomerNotRegisteredException>(()
                => sut.RentMovie(testMovie.Title, testCustomer.SocialSecurityNumber));

            //rentalsMock.DidNotReceive().AddRental(Arg.Any<string>(), Arg.Any<string>());
        }
    }
    
}
