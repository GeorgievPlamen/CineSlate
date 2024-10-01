using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Application.Common.Interfaces;
using Infrastructure.Common.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Common;

public class JwtGenerator(IDateTimeProvider dateTimeProvider, IOptions<JwtSettings> jwtOptions) : IJwtGenerator
{
    private readonly IDateTimeProvider _dateTimeProvider = dateTimeProvider;
    private readonly JwtSettings _jwtSettings = jwtOptions.Value;

    public string GetToken(Guid userId, string firstName, string lastName)
    {
        var signingCredentials = new SigningCredentials(
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Secret))
            , SecurityAlgorithms.HmacSha256);

        Claim[] claims = [
            new Claim(JwtRegisteredClaimNames.Sub, userId.ToString()),
            new Claim(JwtRegisteredClaimNames.GivenName, firstName),
            new Claim(JwtRegisteredClaimNames.FamilyName, lastName),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        ];

        var securityToken = new JwtSecurityToken(
            issuer: _jwtSettings.Issuer,
            audience: _jwtSettings.Audience,
            expires: _dateTimeProvider.Now.AddMinutes(_jwtSettings.ExpiryMinutes).DateTime,
            claims: claims,
            signingCredentials: signingCredentials);

        return new JwtSecurityTokenHandler().WriteToken(securityToken);
    }
}
