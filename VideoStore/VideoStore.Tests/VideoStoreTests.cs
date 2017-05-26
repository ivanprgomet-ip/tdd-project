using System;
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
        [SetUp]
        public void Setup()
        {
            
        }
        [Test]
        public void MovieTitleCanNotBeEmpty()
        {

        }
        [Test]
        public void AddingFourthCopyOfSameMovieNotPossible()
        {

        }
        [Test]
        public void ShouldNotBeAbleToAddSameCustomerTwice()
        {

        }
        [Test]
        public void AddingACustomerMustFollowSSNFormat()
        {

        }

        [Test]
        public void NotBeAbleToRentNonExistentMovie()
        {

        }
        [Test]
        public void UnregisteredCustomerCannotRentMovie()
        {

        }
    }
    [TestFixture]
    public class RentalTests
    {
        [SetUp]
        public void Setup()
        {

        }
        [Test]
        public void BeingAbleToAddRental()
        {

        }
        [Test]
        public void AllRentalsGetA3DayLaterDueDate()
        {

        }
        [Test]
        public void ShouldBeAbleToGetRentalsBySSID()
        {

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
