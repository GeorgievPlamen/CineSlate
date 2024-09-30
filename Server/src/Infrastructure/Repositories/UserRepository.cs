using Application.User.Interfaces;

namespace Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly List<Domain.User.User> _users = [
        new() { Email = "johnDoe@test.com", FirstName = "John", LastName = "Doe", Password = "johnss3cre7" },
        new() { Email = "janeDane@test.com", FirstName = "Jane", LastName = "Dane", Password = "jane0o0" }];
    public List<Domain.User.User> GetUsers()
    {
        return _users;
    }
}