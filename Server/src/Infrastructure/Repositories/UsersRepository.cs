using Application.Users.Interfaces;

namespace Infrastructure.Repositories;

public class UsersRepository : IUsersRepository
{
    private readonly List<Domain.Users.User> _users = [
        new() { Email = "johnDoe@test.com", FirstName = "John", LastName = "Doe", Password = "johnss3cre7" },
        new() { Email = "janeDane@test.com", FirstName = "Jane", LastName = "Dane", Password = "jane0o0" }];
    public List<Domain.Users.User> GetUsers()
    {
        return _users;
    }
}