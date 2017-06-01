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
            testCustomer = new Customer() { Name = "Tess", SSN = "123", Rentals = new List<MovieRentals>() };
        }
        [Test]
        public void MovieTitleCanNotBeEmpty()
        {
            testVideo.Title = "";

            Assert.Throws<MovieTitleCannotBeEmptyException>(() =>
                sut.AddMovie(testVideo));
        }
        [Test]
        public void AddingFourthCopyOfSameMovieNotPossible()
        {
            sut.AddMovie(testVideo);
            sut.AddMovie(testVideo);
            sut.AddMovie(testVideo);

            Assert.Throws<MaximumThreeMoviesException>(() =>
                sut.AddMovie(testVideo));
        }
        [Test]
        public void ShouldNotBeAbleToAddSameCustomerTwice()
        {
            testCustomer.Name = "therese";
            testCustomer.SSN = "123";

            sut.RegisterCustomer(testCustomer.Name, testCustomer.SSN);

            Assert.Throws<CantAddCustomerTwiceException>(()
                => sut.RegisterCustomer(testCustomer.Name, testCustomer.SSN));
        }
        [Test]
        public void AddingACustomerMustFollowSSNFormat()
        {
            testCustomer.Name = "Ivan";
            testCustomer.SSN = "1234-2-2";

            Assert.Throws<SSNFormatException>(() => sut.RegisterCustomer(testCustomer.Name, testCustomer.SSN));
        }

        [Test]
        public void NotBeAbleToRentNonExistentMovie()
        {
            testVideo.Title = "Die Hard";
            Assert.Throws<MovieDoesntExistException>(()
                => sut.RentMovie("non existent movie title","123"));

        }
        [Test]
        public void UnregisteredCustomerCannotRentMovie()
        {
            testVideo.Title = "Dirty dancing";
            testCustomer.SSN = "843";

            var e = Assert.Throws<CustomerNotRegisteredException>(()
                => sut.RentMovie(testVideo.Title, testCustomer.SSN));

            StringAssert.Contains("The customer is not registered", e.Message);
        }
    }
    
}
