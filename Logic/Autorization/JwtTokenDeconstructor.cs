using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Authentication;
using Logic.Abstractions.Autorization;

using Models.Autorization;

namespace Logic.Autorization;

/// <summary>
/// Сущность, которая достаёт из JWT-токена данные пользователя.
/// </summary>
public sealed class JwtTokenDeconstructor : IJwtTokenDeconstructor
{
    /// <inheritdoc/>
    public AutorizationData Deconstruct(JwtToken jwtToken)
    {
        ArgumentNullException.ThrowIfNull(jwtToken);

        var jwtSecurityToken = 
            (JwtSecurityToken)_jwtSecurityTokenHandler.ReadToken(jwtToken.Value);

        var id = jwtSecurityToken.Claims
            .FirstOrDefault(c => c.Type == CLAIM_NAME_USER_ID)
            ?? throw new AuthenticationException("Ошибка авторизации.");
        
        var type = jwtSecurityToken.Claims
            .FirstOrDefault(c => c.Type == CLAIM_NAME_TYPE)
            ?? throw new AuthenticationException("Ошибка авторизации.");

        if (type.Value is CLAIM_NAME_TYPE_CUSTOMER)
        {
            var email = jwtSecurityToken.Claims
                .FirstOrDefault(c => c.Type == CLAIM_NAME_EMAIL)
                ?? throw new AuthenticationException("Ошибка авторизации.");

            return new CustomerAutorizationData(
                new(Convert.ToInt32(id.Value, CultureInfo.InvariantCulture)),
                new(email.Value));
        }

        if (type.Value is not CLAIM_NAME_TYPE_EMPLOYEE)
        {
            throw new AuthenticationException("Ошибка авторизации.");
        }

        var login = jwtSecurityToken.Claims
                .FirstOrDefault(c => c.Type == CLAIM_NAME_LOGIN)
                ?? throw new AuthenticationException("Ошибка авторизации.");

        return new EmployeeAutorizationData(
            new(Convert.ToInt32(id.Value, CultureInfo.InvariantCulture)),
            new(login.Value));
    }

    private JwtSecurityTokenHandler _jwtSecurityTokenHandler = new();

    private const string CLAIM_NAME_USER_ID = "id";
    private const string CLAIM_NAME_EMAIL = "email";
    private const string CLAIM_NAME_LOGIN = "login";
    private const string CLAIM_NAME_TYPE = "type";
    private const string CLAIM_NAME_TYPE_CUSTOMER = "customer";
    private const string CLAIM_NAME_TYPE_EMPLOYEE = "employee";
}
