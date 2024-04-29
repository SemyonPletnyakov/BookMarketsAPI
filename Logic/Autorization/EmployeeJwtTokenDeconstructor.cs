using Logic.Abstractions.Autorization;
using Models.Autorization;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;

namespace Logic.Autorization;

/// <summary>
/// Класс, создающий JWT-токен для сотрудника.
/// </summary>
public sealed class EmployeeJwtTokenDeconstructor :
    IJwtTokenDeconstructor<EmployeeAutorizationData>
{
    /// <inheritdoc/>
    public EmployeeAutorizationData Deconstruct(JwtToken jwtToken)
    {
        var jwtSecurityToken =
            (JwtSecurityToken)_jwtSecurityTokenHandler.ReadToken(jwtToken.Value);

        var id = jwtSecurityToken.Claims
            .FirstOrDefault(c => c.Type == CLAIM_NAME_USER_ID)
            ?? throw new InvalidOperationException("Ошибка авторизации.");

        var email = jwtSecurityToken.Claims
            .FirstOrDefault(c => c.Type == CLAIM_NAME_LOGIN)
            ?? throw new InvalidOperationException("Ошибка авторизации.");

        var type = jwtSecurityToken.Claims
            .FirstOrDefault(c => c.Type == CLAIM_NAME_TYPE)
            ?? throw new InvalidOperationException("Ошибка авторизации.");

        if (type.Value != CLAIM_NAME_TYPE_VALUE)
        {
            throw new InvalidOperationException("Не является сотрудником.");
        }

        return new(new(Convert.ToInt32(id.Value, CultureInfo.InvariantCulture)),
                   new(email.Value));
    }

    private JwtSecurityTokenHandler _jwtSecurityTokenHandler = new();

    private const string CLAIM_NAME_USER_ID = "id";
    private const string CLAIM_NAME_LOGIN = "login";
    private const string CLAIM_NAME_TYPE = "type";
    private const string CLAIM_NAME_TYPE_VALUE = "employee";
}
