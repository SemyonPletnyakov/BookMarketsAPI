using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

using Logic.Abstractions.Autorization;

using Models.Autorization;

namespace Logic.Autorization;

/// <summary>
/// Класс, создающий JWT-токен для покупателя.
/// </summary>
public sealed class CustomerJwtTokenConstructor : 
    IJwtTokenConstructor<CustomerAutorizationData>
{
    /// <summary>
    /// Создаёт объект <see cref="CustomerJwtTokenConstructor"/>.
    /// </summary>
    /// <param name="authConfiguration">
    /// Настроки авторизации.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// Если <paramref name="authConfiguration"/> равен <see langword="null"/>
    /// </exception>
    public CustomerJwtTokenConstructor(IOptions<AuthConfiguration> authConfiguration)
    {
        _authConfiguration = authConfiguration?.Value
            ?? throw new ArgumentNullException(nameof(authConfiguration));
    }

    /// <inheritdoc/>
    public JwtToken Construct(CustomerAutorizationData userData)
    {
        ArgumentNullException.ThrowIfNull(userData);

        var claims = new List<Claim>
        {
            new Claim(CLAIM_NAME_USER_ID,
                      userData.Id.Value.ToString(CultureInfo.InvariantCulture)),

            new Claim(CLAIM_NAME_EMAIL,
                      userData.Email.Value),

            new Claim(CLAIM_NAME_TYPE, CLAIM_NAME_TYPE_VALUE)
        };

        var claimsIdentity = new ClaimsIdentity(claims, "Token");

        var now = DateTime.UtcNow;

        var jwt = new JwtSecurityToken(
            issuer: _authConfiguration.Issuer,
            audience: _authConfiguration.Audience,
            notBefore: DateTime.UtcNow,
            claims: claimsIdentity.Claims,
            expires: now.Add(TimeSpan.FromMinutes(_authConfiguration.Lifetime)),
            signingCredentials: new SigningCredentials(
                new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_authConfiguration.Key)),
                SecurityAlgorithms.HmacSha256));

        var jwtTokenRow = new JwtSecurityTokenHandler().WriteToken(jwt);

        return new(jwtTokenRow);
    }

    private AuthConfiguration _authConfiguration;

    private const string CLAIM_NAME_USER_ID = "id";
    private const string CLAIM_NAME_EMAIL = "email";
    private const string CLAIM_NAME_TYPE = "type";
    private const string CLAIM_NAME_TYPE_VALUE = "customer";
}
