using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using NSubstitute;

namespace VideoStore.Tests
{
    [TestFixture]
    public class VideoStoreTests
    {
        private IVideoStore videoStoreStub { get; set; }
        private Video sutVideo { get; set; }
        private Customer sutCustomer { get; set; }


        [SetUp]
        public void Setup()
        {
            videoStoreStub = Substitute.For<IVideoStore>();
            sutVideo = new Video();
            sutCustomer = new Customer() { Name = "Tess", SSN = "123", Rentals = new List<Rental>() };
        }
        [Test]
        public void MovieTitleCanNotBeEmpty()
        {
            sutVideo.Title = "";

            Assert.Throws<MovieTitleCannotBeEmptyException>(() =>
            videoStoreStub.AddMovie(sutVideo));
        }
        [Test]
        public void AddingFourthCopyOfSameMovieNotPossible()
        {
            videoStoreStub.AddMovie(sutVideo);
            videoStoreStub.AddMovie(sutVideo);
            videoStoreStub.AddMovie(sutVideo);

            Assert.Throws<MaximumThreeMoviesException>(() =>
            videoStoreStub.AddMovie(sutVideo));
        }
        [Test]
        public void ShouldNotBeAbleToAddSameCustomerTwice()
        {
            sutCustomer.Name = "therese";
            sutCustomer.SSN = "123";

            VideoStore.RegisterCustomer(sutCustomer.Name, sutCustomer.SSN);

            Assert.Throws<CantAddCustomerTwiceException>(()
                => VideoStore.RegisterCustomer(sutCustomer.Name, sutCustomer.SSN));
        }
        [Test]
        public void AddingACustomerMustFollowSSNFormat()
        {
            sutCustomer.Name = "Ivan";
            sutCustomer.SSN = "1234-2-2";

            Assert.Throws<SSNFormatException>(() => VideoStore.RegisterCustomer(sutCustomer.Name, sutCustomer.SSN));
        }

        [Test]
        public void NotBeAbleToRentNonExistentMovie()
        {
            sutVideo.Title = "Die Hard";
            Assert.Throws<MovieDoesntExistException>(()
                => videoStoreStub.RentMovie("non existent movie title","123"));

        }
        [Test]
        public void UnregisteredCustomerCannotRentMovie()
        {
            sutVideo.Title = "Dirty dancing";
            sutCustomer.SSN = "843";

            var e = Assert.Throws<CustomerNotRegisteredException>(()
                => videoStoreStub.RentMovie(sutVideo.Title, sutCustomer.SSN));

            StringAssert.Contains("The customer is not registered", e.Message);
        }
    }
    [TestFixture]
    public class RentalTests
    {
        private IVideoStore videoStoreStub { get; set; }
        private IRental rentalStub { get; set; }
        private Video sutVideo { get; set; }
        private Customer sutCustomer { get; set; }

        [SetUp]
        public void Setup()
        {

            videoStoreStub = Substitute.For<IVideoStore>();
            rentalStub = Substitute.For<IRental>();
            sutCustomer = new Customer() { Name = "Tess", SSN = "123", Rentals = new List<Rental>() };
        }
        [Test]
        public void BeingAbleToAddRental()
        {
            Video m = new Video();
            m.Title = "Star wars";
            sutCustomer.Name = "Tess";
            sutCustomer.SSN = "123";

            rentalStub.AddRental(sutVideo.Title, sutCustomer.SSN);

            List<Rental> rents = rentalStub.GetRentalsFor(sutCustomer.SSN);

            Assert.AreEqual(1, rents.Count);
        }
        [Test]
        public void AllRentalsGetA3DayLaterDueDate()
        {
            Rental r = new Rental();
            r.MovieTitle = "Die HArd";
            r.ReturnDate = DateTime.Now.AddDays(3);
            sutCustomer.SSN = "123";

            rentalStub.AddRental(r.MovieTitle, sutCustomer.SSN);

            Assert.AreEqual(DateTime.Now.AddDays(3).Date, r.ReturnDate.Date);


        }
        [Test]
        public void ShouldBeAbleToGetRentalsBySSN()
        {
            Customer c = new Customer();
            c.Name = "Ivan";
            c.SSN = "123";
            c.Rentals.Add(new Rental() { MovieTitle = "Die hard", ReturnDate = DateTime.Now.AddDays(3) });
            c.Rentals.Add(new Rental() { MovieTitle = "Titanic", ReturnDate = DateTime.Now.AddDays(3) });


            rentalStub.GetRentalsFor("123").Returns(new List<Rental>()
            {
                new Rental { MovieTitle = "Die hard", ReturnDate = DateTime.Now.AddDays(3) },
                new Rental { MovieTitle = "Titanic", ReturnDate = DateTime.Now.AddDays(3) },
            });


            //List<Rental> listOfRentals = rentalStub.GetRentals("123");
            //Assert.AreEqual(2, c.Rentals.Count);
            //StringAssert.Contains("Die hard", c.Rentals[0].MovieTitle);
        }

        [Test]
        public void CanRentMoreThanOneMovie()
        {
            Video v1 = new Video() { Title = "dirty dancing" };
            Video v2 = new Video() { Title = "titanic" };
            Customer c1 = new Customer() { Name = "ivan", SSN = "123", Rentals = new List<Rental>() };

            rentalStub.AddRental(v1.Title, c1.SSN);

            Assert.AreEqual(2, c1.Rentals.Count);
        }
        [Test]
        public void CannotRentMoreThan3Movies()
        {
            Rental r = new Rental();
            r.MovieTitle = "Die HArd";
            r.ReturnDate = DateTime.Now.AddDays(3);
            Rental r1 = new Rental();
            r.MovieTitle = "Titanic";
            r.ReturnDate = DateTime.Now.AddDays(3);
            Rental r2 = new Rental();
            r.MovieTitle = "Dirty Dancing";
            r.ReturnDate = DateTime.Now.AddDays(3);
            Rental r3 = new Rental();
            r.MovieTitle = "Star Wars";
            r.ReturnDate = DateTime.Now.AddDays(3);
            sutCustomer.SSN = "123";
            rentalStub.AddRental(r.MovieTitle, sutCustomer.SSN);
            rentalStub.AddRental(r1.MovieTitle, sutCustomer.SSN);
            rentalStub.AddRental(r2.MovieTitle, sutCustomer.SSN);

            Assert.Throws<MaximumThreeMoviesToRentalException>(() =>
                rentalStub.AddRental(r3.MovieTitle, sutCustomer.SSN));
        }
        [Test]
        public void CustomersMayNotPossessTwoCopiesOfTheSameMovie()
        {
            Video v1 = new Video() { Title = "die hard" };
            Customer c1 = new Customer() { Name = "ivan", SSN = "123", Rentals = new List<Rental>() };


            rentalStub.AddRental(v1.Title, c1.SSN);

            Assert.Throws<CantPossessTwoCopiesOfSameVideoException>(()
                => rentalStub.AddRental(v1.Title, c1.SSN));

            Assert.AreEqual(1, c1.Rentals.Count);
        }
        [Test]
        public void CustomersMayNotRentAnymoreMoviesIfTheyHaveLateDueDateMovies()
        {
            

        }

        // more tests here
    }
}
