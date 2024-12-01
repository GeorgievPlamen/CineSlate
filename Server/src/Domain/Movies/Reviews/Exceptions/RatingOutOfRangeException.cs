namespace Domain.Movies.Reviews.Exceptions;

public class RatingOutOfRangeException(string? message = "Must be between 1 and 5!") : ArgumentOutOfRangeException(message)
{
}