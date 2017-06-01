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
        private Movie testVideo { get; set; }
        private Customer testCustomer { get; set; }
        private IMovieRentals rentalsMock { get; set; }


        [SetUp]
        public void Setup()
        {
            rentalsMock = Substitute.For<IMovieRentals>();
            sut = new Gui.VideoStore(rentalsMock);
            testVideo = new Movie();
            testCustomer = new Customer() { Name = "Tess", SocialSecurityNumber = "123", Rentals = new List<MovieRentals>() };
        }
        [Test]
        public void CannotAddEmptyMovieTitle()
        {
            testVideo.Title = "";

            Assert.Throws<MovieTitleCannotBeEmptyException>(() =>
                sut.AddMovie(testVideo));
        }
        [Test]
        public void CannotAddFourthCopyOfSameMovie()
        {
            sut.AddMovie(testVideo);
            sut.AddMovie(testVideo);
            sut.AddMovie(testVideo);

            Assert.Throws<MaximumThreeMoviesException>(() =>
                sut.AddMovie(testVideo));
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
            testVideo.Title = "Die Hard";
            Assert.Throws<MovieDoesntExistException>(()
                => sut.RentMovie("non existent movie title","123"));

        }
        [Test]
        public void CannotRentMovieAsAnUnregisteredCustomer()
        {
            testVideo.Title = "Dirty dancing";
            testCustomer.SocialSecurityNumber = "843";

            var e = Assert.Throws<CustomerNotRegisteredException>(()
                => sut.RentMovie(testVideo.Title, testCustomer.SocialSecurityNumber));

            StringAssert.Contains("The customer is not registered", e.Message);
        }
    }
    
}
