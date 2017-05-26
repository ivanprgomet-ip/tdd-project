﻿using System;

namespace VideoStore.Tests
{
    public class MovieTitleCannotBeEmptyException : Exception
    {
        public MovieTitleCannotBeEmptyException(string msg) : base(msg)
        {

        }
    }
    public class MaximumThreeMoviesException : Exception
    {

    }

    public class CantAddCustomerTwiceException : Exception
    {

    }
    public class SSNFormatException : Exception
    {

    }

    public class MovieDoesntExistException : Exception
    {

    }

    public class CustomerNotRegisteredException : Exception
    {

    }

}