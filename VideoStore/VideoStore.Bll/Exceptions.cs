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
        public CantAddCustomerTwiceException(string msg) : base(msg)
        {

        }
    }
    public class CantPossessTwoCopiesOfSameVideoException : Exception
    {

    }
    public class SSNFormatException : Exception
    {
        public SSNFormatException(string msg) : base(msg)
        {
            
        }
    }

    public class MovieDoesntExistException : Exception
    {

    }

    public class CustomerNotRegisteredException : Exception
    {
        public CustomerNotRegisteredException(string msg) : base(msg)
        {
            
        }
    }

    public class MaximumThreeMoviesToRentalException : Exception
    {
        
    }

    public class MovieException : Exception
    {
        public MovieException(string msg) : base(msg)
        {

        }
    }

    public class MovieWithExpiredDateFoundException:Exception
    {

    }

}