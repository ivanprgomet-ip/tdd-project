namespace VideoStore.Bll
{
    public class Movie
    {
        public string Title { get; set; }

        public Movie()
        {
            
        }
        public Movie(string title)
        {
            this.Title = title;
        }
    }
}