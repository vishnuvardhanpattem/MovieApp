using MovieApp.Domain;

namespace MovieApp.Repository
{
    public class MovieRepository
    {
        // Movie Collection dictionary to store the movies
        private readonly Dictionary<int, Movie> _movieCollections = new Dictionary<int, Movie>();
    //    private readonly Dictionary<string, List<Movie>> _moviesByLanguage =
    //new Dictionary<string, List<Movie>>(StringComparer.OrdinalIgnoreCase);



        public void AddMovie(int movieId, Movie movie)
        {
            _movieCollections[movieId] = movie;
        }
        public void RemoveMovie(int movieId)
        {
            _movieCollections.Remove(movieId);
        }

        public Movie? GetMovieById(int movieId)
        {
            return _movieCollections[movieId];
        }

        public List<Movie> GetAllMovies()
        {
            return _movieCollections.Values.ToList();
        }

        public List<Movie>? GetMoviesByLanguage(string language)
        {
            return _movieCollections.Values
                .Where(movie => movie.Language == language)
                .ToList();
        }

        public Dictionary<string, int>? GetTotalCountOfMoviesByLanguage()
        {
            return _movieCollections.Values
                .GroupBy(movie => movie.Language)
                .ToDictionary(group => group.Key, group => group.Count());
        }

        public Dictionary<string, List<Movie>>? GetTotalMoviesGroupedByLanguage()
        {
            return _movieCollections.Values.GroupBy(movie => movie.Language).ToDictionary(group => group.Key, group => group.ToList());
        }
    }
}
