using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using NUnit.Framework;
using NSubstitute;
using VideoStore.Bll;

namespace VideoStore.Tests
{
    [TestFixture]
    [Category("TESTVideoStore")]
    public class VideoStoreTests
    {
        private Bll.VideoStore sut { get; set; }
        private Movie testMovie { get; set; }
        private Customer testCustomer { get; set; }
        private IMovieRentals rentalsMock { get; set; }
        


        [SetUp]
        public void Setup()
        {
            rentalsMock = Substitute.For<IMovieRentals>();
            sut = new Bll.VideoStore(rentalsMock);
            testMovie = new Movie{Title = "Die Hard"};
            testCustomer = new Customer() { Name = "Tess", SocialSecurityNumber = "1991-02-23"};
        }
        [Test]
        public void CannotAddEmptyMovieTitle()
        {
            testMovie.Title = "";

            Assert.Throws<MovieException>(() =>
                sut.AddMovie(testMovie));
        }
        [Test]
        public void CannotAddFourthCopyOfSameMovie()
        {
            sut.AddMovie(testMovie);
            sut.AddMovie(testMovie);
            sut.AddMovie(testMovie);

            Assert.Throws<MovieException>(() =>
                sut.AddMovie(testMovie));
        }
        [Test]
        public void CannotAddSameCustomerTwice()
        {
            sut.RegisterCustomer(testCustomer.Name, testCustomer.SocialSecurityNumber);

            Assert.Throws<CantAddCustomerTwiceException>(()
                => sut.RegisterCustomer(testCustomer.Name, testCustomer.SocialSecurityNumber));
        }
        [Test]
        public void MustFollowSSNFormatWhenRegisteringNewCustomer()
        {
            testCustomer.SocialSecurityNumber = "1234-2-2";

            Assert.Throws<SSNFormatException>(() => sut.RegisterCustomer(testCustomer.Name, testCustomer.SocialSecurityNumber));
        }

        [Test]
        public void CannotRentNonExistentMovie()
        { 
            sut.RegisterCustomer(testCustomer.Name,testCustomer.SocialSecurityNumber);
            Assert.Throws<MovieException>(()
                => sut.RentMovie("Titanic",testCustomer.SocialSecurityNumber));

            rentalsMock.DidNotReceive().AddRental(Arg.Any<string>(), Arg.Any<string>());
        }
        [Test]
        public void CannotRentMovieAsAnUnregisteredCustomer()
        {
            sut.AddMovie(testMovie);
            
            Assert.Throws<MovieException>(()
                => sut.RentMovie(testMovie.Title, testCustomer.SocialSecurityNumber));

            rentalsMock.DidNotReceive().AddRental(Arg.Any<string>(), Arg.Any<string>());
        }
        
    }
    
}
