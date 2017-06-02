using System;

namespace VideoStore.Bll
{
    public class MovieTitleCannotBeEmptyException : Exception
    {
        //public MovieTitleCannotBeEmptyException(string msg) : base(msg)
        //{

        //}
    }
    public class MaximumThreeMoviesException : Exception
    {

    }

    public class CantAddCustomerTwiceException : Exception
    {

    }
    public class CantPossessTwoCopiesOfSameVideoException : Exception
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

    public class MaximumThreeMoviesToRentalException : Exception
    {
        
    }

    public class MovieException : Exception
    {
        
    }

}