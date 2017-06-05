using System;

namespace VideoStore.Bll
{
    public class Movie:IEquatable<Movie>
    {
        public string Title { get; set; }

        public Movie()
        {
            
        }
        public Movie(string title)
        {
            this.Title = title;
        }

        public bool Equals(Movie other)
        {
            if (other == null)
            {
                return false;
            }
            var result = this.Title==other.Title;
            return result;
        }
    }
}