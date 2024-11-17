namespace Infrastructure.MoviesClient;

public record TMDBMovie(
    bool Adult,
    string Backdrop_path,
    int[] Genre_ids,
    int Id,
    string Original_language,
    string Original_title,
    string Overview,
    double Popularity,
    string Poster_path,
    DateOnly Release_date,
    string Title,
    bool Video,
    double Vote_average,
    int Vote_count);

public record TMDBPopularMovies(
    int Page,
    TMDBMovie[] Results,
    int Total_Pages,
    int Total_Results
);