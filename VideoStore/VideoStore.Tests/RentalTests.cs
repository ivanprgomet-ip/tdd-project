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
    public class RentalTests
    {
        //Behöver inte ha Video och Customer här.
        private MovieRentals sut { get; set; }
        private Movie testVideo { get; set; }
        private Customer testCustomer { get; set; }

        [SetUp]
        public void Setup()
        {
            sut = new MovieRentals();
            testCustomer = new Customer() { Name = "Tess", SSN = "123", Rentals = new List<MovieRentals>() };
        }
        [Test]
        public void CanAddRental()
        {
            Movie m = new Movie();
            m.Title = "Star wars";
            testCustomer.Name = "Tess";
            testCustomer.SSN = "123";

            sut.AddRental(testVideo.Title, testCustomer.SSN);

            List<MovieRentals> rents = sut.GetRentalsFor(testCustomer.SSN);

            Assert.AreEqual(1, rents.Count);
        }
        [Test]
        public void AllRentalsGetA3DayLaterDueDate()
        {
            MovieRentals r = new MovieRentals();
            r.MovieTitle = "Die HArd";
            r.ReturnDate = DateTime.Now.AddDays(3);
            testCustomer.SSN = "123";

            sut.AddRental(r.MovieTitle, testCustomer.SSN);

            Assert.AreEqual(DateTime.Now.AddDays(3).Date, r.ReturnDate.Date);


        }
        [Test]
        public void ShouldBeAbleToGetRentalsBySSN()
        {
            Customer c = new Customer();
            c.Name = "Ivan";
            c.SSN = "123";
            c.Rentals.Add(new MovieRentals() { MovieTitle = "Die hard", ReturnDate = DateTime.Now.AddDays(3) });
            c.Rentals.Add(new MovieRentals() { MovieTitle = "Titanic", ReturnDate = DateTime.Now.AddDays(3) });


            var result=sut.GetRentalsFor("123");


            //List<Rental> listOfRentals = rentalStub.GetRentals("123");
            //Assert.AreEqual(2, c.Rentals.Count);
            //StringAssert.Contains("Die hard", c.Rentals[0].MovieTitle);
        }

        [Test]
        public void CanRentMoreThanOneMovie()
        {
            Movie v1 = new Movie() { Title = "dirty dancing" };
            Movie v2 = new Movie() { Title = "titanic" };
            Customer c1 = new Customer() { Name = "ivan", SSN = "123", Rentals = new List<MovieRentals>() };

            sut.AddRental(v1.Title, c1.SSN);

            Assert.AreEqual(2, c1.Rentals.Count);
        }
        [Test]
        public void CannotRentMoreThan3Movies()
        {
            MovieRentals r = new MovieRentals();
            r.MovieTitle = "Die HArd";
            r.ReturnDate = DateTime.Now.AddDays(3);
            MovieRentals r1 = new MovieRentals();
            r.MovieTitle = "Titanic";
            r.ReturnDate = DateTime.Now.AddDays(3);
            MovieRentals r2 = new MovieRentals();
            r.MovieTitle = "Dirty Dancing";
            r.ReturnDate = DateTime.Now.AddDays(3);
            MovieRentals r3 = new MovieRentals();
            r.MovieTitle = "Star Wars";
            r.ReturnDate = DateTime.Now.AddDays(3);
            testCustomer.SSN = "123";
            sut.AddRental(r.MovieTitle, testCustomer.SSN);
            sut.AddRental(r1.MovieTitle, testCustomer.SSN);
            sut.AddRental(r2.MovieTitle, testCustomer.SSN);

            Assert.Throws<MaximumThreeMoviesToRentalException>(() =>
                sut.AddRental(r3.MovieTitle, testCustomer.SSN));
        }
        [Test]
        public void CustomersMayNotPossessTwoCopiesOfTheSameMovie()
        {
            Movie v1 = new Movie() { Title = "die hard" };
            Customer c1 = new Customer() { Name = "ivan", SSN = "123", Rentals = new List<MovieRentals>() };


            sut.AddRental(v1.Title, c1.SSN);

            Assert.Throws<CantPossessTwoCopiesOfSameVideoException>(()
                => sut.AddRental(v1.Title, c1.SSN));

            Assert.AreEqual(1, c1.Rentals.Count);
        }
        [Test]
        public void CustomersMayNotRentAnymoreMoviesIfTheyHaveLateDueDateMovies()
        {


        }

        // more tests here
    }
}
