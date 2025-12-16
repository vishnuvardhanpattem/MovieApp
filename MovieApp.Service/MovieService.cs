using MovieApp.Domain;
using MovieApp.Repository;
using System.ComponentModel;
using System.Text;
using System.Text.Json;


// Delegate 
public delegate void AckDelegate(string message);

namespace MovieApp.Service
{
    public class MovieService
    {
        // Delegate variable
        public AckDelegate Delegate { get; set; }

        // MovieRepository variavble
        private MovieRepository _movieRepository;

        // Constructor dependency injection
        public MovieService(MovieRepository movieRepository, AckDelegate ackDelegate)
        {
            _movieRepository = movieRepository;
            Delegate = ackDelegate;
        }

        // Adding movie into movie Collection
        public void AddMovie(string title, string director, string genre, string language, string? actor = null, string? actress = null, DateTime? releaseDate = null)
        {
            Movie movie = new Movie
            {
                Title = title.ToUpper(),
                Director = director.ToUpper(),
                Genre = genre.ToUpper(),
                Language = language.ToUpper(),
                Actor = actor?.ToUpper(),
                Actress = actress?.ToUpper(),
                ReleaseDate = releaseDate
            };

            _movieRepository.AddMovie(movie.MovieId, movie);
            Delegate($"Movie with id : {movie.MovieId} is successfully added to the movie collection with Title : {movie.Title}");
        }

        // Deleting the movie by id
        public void DeleteMovie(int movieId)
        {
            Movie? movie = _movieRepository.GetMovieById(movieId);
            if(movie is  null)
            {
                Delegate($"Movie with given id : {movieId} is not found in the movie collection. ");
                return;
            }
            _movieRepository.RemoveMovie(movie.MovieId);
            Delegate($"{movie.Title} Movie with id : {movieId} is successfully deleted. ");
        }

        // Get All Movie List in the movie Collection
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

        // Get Movies by Id in Movie Collection
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

        // Get Movies by Language and storing in the list then converting to the Json 
        public void GetMoviesByLanguage(string language)
        {
            List<Movie>? movieList = _movieRepository.GetMoviesByLanguage(language.ToUpper());
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

        // Get Count of the Total Movies in Collection
        public void GetTotalNumberOfMovies()
        {
            List<Movie> movieList = _movieRepository.GetAllMovies();
            int totalMovies = movieList.Count;
            Delegate($"Total number of movies in the movie collection is : {totalMovies}");
        }

        // Get Total Count of movies grouped by language
        public void GetTotalCountOfMoviesByLanguage()
        {
            var movieList = _movieRepository.GetTotalCountOfMoviesByLanguage();
            if (movieList is null)
            {
                Delegate($"There is no any movies in the collection");
                return;
            }
            int totalMoviesByLanguage = movieList.Count;
            var options = new JsonSerializerOptions
            {
                WriteIndented = true
            };
            string movieListDetailsInJson = JsonSerializer.Serialize(movieList, options);
            Delegate($"Total Movies in Collection is {totalMoviesByLanguage} \n{movieListDetailsInJson}");
        }

        // Get Total Movies Grouped By Language
        public void GetTotalMoviesGroupedByLanguage()
        {
            var movieList = _movieRepository.GetTotalMoviesGroupedByLanguage();
            if (movieList is null)
            {
                Delegate($"There is no any movies in the collection");
                return;
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
