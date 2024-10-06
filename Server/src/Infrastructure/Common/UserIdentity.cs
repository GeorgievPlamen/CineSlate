using Application.Users.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Application.Common.Interfaces;
using Infrastructure.Common.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
namespace Infrastructure.Common;

public class UserIdentity(
    IDateTimeProvider dateTimeProvider,
    IOptions<JwtSettings> jwtOptions) : IUserIdentity
{
    private readonly IDateTimeProvider _dateTimeProvider = dateTimeProvider;
    private readonly JwtSettings _jwtSettings = jwtOptions.Value;

    public string GenerateJwtToken(Guid userId, string firstName, string lastName, string email, string role)
    {
        var signingCredentials = new SigningCredentials(
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Secret))
            , SecurityAlgorithms.HmacSha256);

        Claim[] claims = [
            new Claim(JwtRegisteredClaimNames.Sub, userId.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, email),
            new Claim(JwtRegisteredClaimNames.GivenName, firstName),
            new Claim(JwtRegisteredClaimNames.FamilyName, lastName),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(ClaimTypes.Role, role),
        ];

        var securityToken = new JwtSecurityToken(
            issuer: _jwtSettings.Issuer,
            audience: _jwtSettings.Audience,
            expires: _dateTimeProvider.Now.AddMinutes(_jwtSettings.ExpiryMinutes).DateTime,
            claims: claims,
            signingCredentials: signingCredentials);

        return new JwtSecurityTokenHandler().WriteToken(securityToken);
    }

    public string HashPassword(string password)
    {
        byte[] salt = new byte[16];

        using var rng = RandomNumberGenerator.Create();

        string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
            password: password,
            salt: salt,
            prf: KeyDerivationPrf.HMACSHA256,
            iterationCount: 100000,
            numBytesRequested: 16));

        var base64 = Convert.ToBase64String(salt);

        return $"{base64}.{hashed}";
    }

    public bool ValidatePassword(string password, string storedPassword)
    {
        var parts = storedPassword.Split('.');

        if (parts.Length != 2)
        {
            return false;
        }

        byte[] salt = Convert.FromBase64String(parts[0]);
        string storedHashedPassword = parts[1];

        string hashedPassword = Convert.ToBase64String(KeyDerivation.Pbkdf2(
            password: password,
            salt: salt,
            prf: KeyDerivationPrf.HMACSHA256,
            iterationCount: 100000,
            numBytesRequested: 16));

        return hashedPassword == storedHashedPassword;
    }
}
