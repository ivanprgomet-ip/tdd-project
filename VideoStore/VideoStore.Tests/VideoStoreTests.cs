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
        private IVideoStore videoStoreMock { get; set; }
        private Video sutVideo { get; set; }
        private Customer sutCustomer { get; set; }


        [SetUp]
        public void Setup()
        {
            videoStoreMock = Substitute.For<IVideoStore>();
            sutVideo = new Video();
            sutCustomer = new Customer() { Name = "Tess", SSN = "123", Rentals = new List<Rental>() };
        }
        [Test]
        public void MovieTitleCanNotBeEmpty()
        {
            sutVideo.Title = "";

            Assert.Throws<MovieTitleCannotBeEmptyException>(() =>
                videoStoreMock.AddMovie(sutVideo));
        }
        [Test]
        public void AddingFourthCopyOfSameMovieNotPossible()
        {
            videoStoreMock.AddMovie(sutVideo);
            videoStoreMock.AddMovie(sutVideo);
            videoStoreMock.AddMovie(sutVideo);

            Assert.Throws<MaximumThreeMoviesException>(() =>
                videoStoreMock.AddMovie(sutVideo));
        }
        [Test]
        public void ShouldNotBeAbleToAddSameCustomerTwice()
        {
            sutCustomer.Name = "therese";
            sutCustomer.SSN = "123";

            videoStoreMock.RegisterCustomer(sutCustomer.Name, sutCustomer.SSN);

            Assert.Throws<CantAddCustomerTwiceException>(()
                => videoStoreMock.RegisterCustomer(sutCustomer.Name, sutCustomer.SSN));
        }
        [Test]
        public void AddingACustomerMustFollowSSNFormat()
        {
            sutCustomer.Name = "Ivan";
            sutCustomer.SSN = "1234-2-2";

            Assert.Throws<SSNFormatException>(() => videoStoreMock.RegisterCustomer(sutCustomer.Name, sutCustomer.SSN));
        }

        [Test]
        public void NotBeAbleToRentNonExistentMovie()
        {
            sutVideo.Title = "Die Hard";
            Assert.Throws<MovieDoesntExistException>(()
                => videoStoreMock.RentMovie("non existent movie title","123"));

        }
        [Test]
        public void UnregisteredCustomerCannotRentMovie()
        {
            sutVideo.Title = "Dirty dancing";
            sutCustomer.SSN = "843";

            var e = Assert.Throws<CustomerNotRegisteredException>(()
                => videoStoreMock.RentMovie(sutVideo.Title, sutCustomer.SSN));

            StringAssert.Contains("The customer is not registered", e.Message);
        }
    }
    
}
