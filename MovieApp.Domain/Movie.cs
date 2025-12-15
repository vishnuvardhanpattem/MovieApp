namespace MovieApp.Domain
{
    public class Movie
    {
        private static int _idCounter = 1;
        public int MovieId {  get; private set; }
        public string Title { get;  set; }

        public string Director { get; set; }

        public string Genre { get; set; }

        public string Language { get; set; }

        public string? Actor { get; set; }

        public string? Actress { get; set; }

        public DateTime? ReleaseDate { get; set; }

        //public Movie(string title, string director, string genre, string language, string? actor = null, string? actress = null, DateTime? releaseDate = null)
        //{
        //    MovieId = _idCounter++;
        //    Title = title.ToUpper();
        //    Director = director.ToUpper();
        //    Genre = genre.ToUpper(); 
        //    Language = language.ToUpper();
        //    Actor = actor?.ToUpper();
        //    Actress = actress?.ToUpper();
        //    ReleaseDate = releaseDate;
        //}

        public Movie()
        {
            MovieId = _idCounter++;
        }
    }
}
