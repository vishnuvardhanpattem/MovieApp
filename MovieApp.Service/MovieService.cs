using MovieApp.Domain;
using MovieApp.Repository;
using System.Text;
using System.Text.Json;

public delegate void AckDelegate(string message);

namespace MovieApp.Service
{
    public class MovieService
    {
        public AckDelegate Delegate { get; set; }
        private MovieRepository _movieRepository;

        public MovieService(MovieRepository movieRepository, AckDelegate ackDelegate)
        {
            _movieRepository = movieRepository;
            Delegate = ackDelegate;
        }


        public void AddMovie(string title, string director, string genre, string language, string? actor = null, string? actress = null, DateTime? releaseDate = null)
        {
            Movie movie = new Movie
            {
                Title = title,
                Director = director,
                Genre = genre,
                Language = language,
                Actor = actor,
                Actress = actress,
                ReleaseDate = releaseDate
            };

            _movieRepository.AddMovie(movie.MovieId, movie);
            Delegate($"Movie with id : {movie.MovieId} is successfully added to the movie collection with Title : {movie.Title}");
        }

        public void DeleteMovie(int movieId)
        {
            Movie? movie = _movieRepository.GetMovieById(movieId);
            if(movie is  null)
            {
                Delegate($"Movie with given id : {movieId} is not found in the movie collection. ");
                return;
            }
            _movieRepository.RemoveMovie(movie.MovieId);
            Delegate($"Movie with id : {movieId} is successfully deleted. ");
        }

        public void GetAllMovieList()
        {
            List<Movie> movieList = _movieRepository.GetAllMovies();
            if (movieList == null)
            {
                Delegate($"Cannot find any movies. Movie Collection is empty. ");
                return ;
            }
            var options = new JsonSerializerOptions
            {
                WriteIndented = true
            };
            string movieListDetailsInJson = JsonSerializer.Serialize(movieList, options);
            Delegate(movieListDetailsInJson);
        }

        public void GetMovieById(int movieId)
        {
            Movie? movie = _movieRepository.GetMovieById(movieId);
            if (movie is null)
            {
                Delegate($"Movie with given id : {movieId} is not found in the movie collection. ");
                return;
            }
            var options = new JsonSerializerOptions
            {
                WriteIndented = true
            };
            string movieDetailsInJson = JsonSerializer.Serialize(movie, options);
            Delegate(movieDetailsInJson);
        }

        public void GetMoviesByLanguage(string language)
        {
            List<Movie>? movieList = _movieRepository.GetMoviesByLanguage(language);
            if(movieList is null)
            {
                Delegate($"There is no any movies with language : {language}");
            }
            var options = new JsonSerializerOptions
            {
                WriteIndented = true
            };
            string movieListDetailsInJson = JsonSerializer.Serialize(movieList, options);
            Delegate(movieListDetailsInJson);
        }
    }
}
