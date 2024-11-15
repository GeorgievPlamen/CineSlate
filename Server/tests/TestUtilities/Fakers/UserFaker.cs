
using Bogus;
using Domain.Users;
using Domain.Users.Enums;

namespace TestUtilities.Fakers;

public static class UserFaker
{
    public static User GenerateValid() => new Faker<User>()
        .CustomInstantiator(f => User.Create(
            f.Person.FirstName,
            f.Person.LastName,
            f.Person.Email,
            Constants.ValidPassword,
            Roles.User))
        .Generate();
}