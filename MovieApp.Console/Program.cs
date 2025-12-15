//See https://aka.ms/new-console-template for more information
using MovieApp.Repository;
using MovieApp.Service;
using System;
using System.Globalization;
using System.Xml.Linq;




Console.WriteLine("==================Movie Manager Console==================");

MovieRepository movieRepository = new MovieRepository();

AckDelegate ack = (msg) => Console.WriteLine(msg);

MovieService movieService = new MovieService(movieRepository, ack);

while (true)
{
    Console.WriteLine("Choose the Options");
    Console.WriteLine("1.Add Movie \n2.Remove Movie \n3.Get Movie By Id \n4.Get All Movies \n5.Get All Movies By Language \n6.Exit");
    int operation = int.Parse(Console.ReadLine());
    switch (operation)
    {
        case 1: Console.WriteLine("Enter the Movie Details To add into Movie Collection");
            Console.WriteLine("Enter the Title : ");
            string title = Console.ReadLine();
            Console.WriteLine("Enter the director name : ");
            string directorName = Console.ReadLine();
            Console.WriteLine("Enter the movie genre : ");
            string genre = Console.ReadLine();
            Console.WriteLine("Enter the movie language : ");
            string language = Console.ReadLine();
            Console.WriteLine("Enter the actor name : ");
            string actorName = Console.ReadLine();
            Console.WriteLine("Enter the heroine name : ");
            string actressName = Console.ReadLine();
            Console.WriteLine("Enter the Release year in (YYYY-MM-DD)");
            try
            {
                string input = Console.ReadLine();
                DateTime? releaseDate;

                if (string.IsNullOrWhiteSpace(input))
                {
                    releaseDate = null;
                }
                else
                {
                    if (!DateTime.TryParseExact(
                            input,
                            "yyyy-MM-dd",
                            CultureInfo.InvariantCulture,
                            DateTimeStyles.None,
                            out DateTime parsedDate))
                    {
                        throw new FormatException("Invalid date format. Expected format: YYYY-MM-DD");
                    }

                    releaseDate = parsedDate;
                }

                movieService.AddMovie(
                    title,
                    directorName,
                    genre,
                    language,
                    actorName,
                    actressName,
                    releaseDate
                );
            }
            catch (FormatException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error: {ex.Message}");
            }

            break;

        case 2: Console.WriteLine("Enter the Movie Id to remove the movie from collection");
            try
            {
                int movieId = int.Parse(Console.ReadLine());
                movieService.DeleteMovie(movieId);
            }
            catch (FormatException)
            {
                throw new FormatException("Invalid Movie Id. Please enter a number.");
            }
            break;
        case 3: Console.WriteLine("Enter the Movie Id to get details");
            try
            {
                int movieId = int.Parse(Console.ReadLine());
                movieService.GetMovieById(movieId);
            }
            catch (FormatException)
            {
                throw new FormatException("Invalid Movie Id. Please enter a number.");
            }
            break;
        case 4: Console.WriteLine("Here is the listed movies in Movie Collection");
            movieService.GetAllMovieList();
            break;
        case 5: Console.WriteLine("Get all Movies by Languages");
            string movieLanguage = Console.ReadLine();
            movieService.GetMoviesByLanguage(movieLanguage);
            break;

        default: Console.WriteLine("Exit");
            return;

    }
}
