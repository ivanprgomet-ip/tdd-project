﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace VideoStore.Tests
{
    [TestFixture]
    public class VideoStoreTests
    {
            private Movie sutMovie { get; set; }
            private VideoStore sutVideoStore { get; set; }
            private Customer sutCustomer { get; set; }
        
        [SetUp]
        public void Setup()
        {
            sutMovie = new Movie();
            sutVideoStore = new VideoStore();
        }
        [Test]
        public void MovieTitleCanNotBeEmpty()
        {
            sutMovie.Title = "";

            Assert.Throws<MovieTitleCannotBeEmptyException>(() =>
            sutVideoStore.AddMovie(sutMovie));
        }
        [Test]
        public void AddingFourthCopyOfSameMovieNotPossible()
        {
            sutVideoStore.AddMovie(sutMovie);
            sutVideoStore.AddMovie(sutMovie);
            sutVideoStore.AddMovie(sutMovie);

            Assert.Throws<MaximumThreeMoviesException>(() =>
            sutVideoStore.AddMovie(sutMovie));
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
            sutMovie.Title = "Die Hard";
            Assert.Throws<MovieDoesntExistException>(()
                => sutVideoStore.RentMovie(sutMovie));

        }
        [Test]
        public void UnregisteredCustomerCannotRentMovie()
        {
            sutMovie.Title = "Dirty dancing";
            sutCustomer.SSN = "843";

            var e = Assert.Throws<CustomerNotRegisteredException>(()
                => sutVideoStore.RentMovie(sutMovie.Title, sutCustomer.SSN));

            StringAssert.Contains("The customer is not registered", e.Message);
        }
    }
    [TestFixture]
    public class RentalTests
    {
        private Movie sutMovie { get; set; }
        private VideoStore sutVideoStore { get; set; }
        private Customer sutCustomer { get; set; }
        private Rental sutRental { get; set; }

        [SetUp]
        public void Setup()
        {
            sutRental = new Rental();

        }
        [Test]
        public void BeingAbleToAddRental()
        {
            Movie m = new Movie();
            m.Title = "Star wars";
            sutCustomer.Name = "Tess";
            sutCustomer.SSN = "123";

            sutRental.AddRental(sutMovie.Title, sutCustomer.SSN);

            Movie retrieved = sutVideoStore.ReturnMovie(sutMovie.Title, sutCustomer.SSN);

            Assert.AreEqual("Star wars", retrieved.Title);
        }
        [Test]
        public void AllRentalsGetA3DayLaterDueDate()
        {
            Rental r =new  Rental();
            r.MovieTitle = "Die HArd";
            r.ReturnDate = DateTime.Now.AddDays(3);
            sutCustomer.SSN = "123";

            sutRental.AddRental(r.MovieTitle, sutCustomer.SSN);

            Assert.AreEqual(DateTime.Now.AddDays(3).Date,r.ReturnDate.Date);


        }
        [Test]
        public void ShouldBeAbleToGetRentalsBySSN()
        {
            Customer c = new Customer();
            c.Name = "Ivan";
            c.SSN = "123";
            c.Rentals.Add(new Rental() { MovieTitle = "Die hard", ReturnDate = DateTime.Now.AddDays(3)});
            c.Rentals.Add(new Rental() { MovieTitle = "Titanic", ReturnDate = DateTime.Now.AddDays(3)});

            List<Rental> listOfRentals = sutRental.GetRentals("123");
            Assert.AreEqual(2,c.Rentals.Count);
            StringAssert.Contains("Die hard",c.Rentals[0].MovieTitle);
        }

        [Test]
        public void CanRentMoreThanOneMovie()
        {

        }
        [Test]
        public void CannotRentMoreThan3Movies()
        {

        }
        [Test]
        public void CustomersMayNotPossessTwoCopiesOfTheSameMovie()
        {

        }
        [Test]
        public void CustomersMayNotRentAnymoreMoviesIfTheyHaveLateDueDateMovies()
        {
            //Assert.Throws<Exceptionx>(() => sut.DoStuff());
        }

        // more tests here
    }
}
