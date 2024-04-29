using System.Globalization;
using System;
using System.IdentityModel.Tokens.Jwt;

using Logic.Abstractions.Autorization;

using Models.Autorization;

namespace Logic.Autorization;

/// <summary>
/// Класс, создающий JWT-токен для покупателя.
/// </summary>
public sealed class CustomerJwtTokenDeconstructor : 
    IJwtTokenDeconstructor<CustomerAutorizationData>
{
    /// <inheritdoc/>
    public CustomerAutorizationData Deconstruct(JwtToken jwtToken)
    {
        var jwtSecurityToken = 
            (JwtSecurityToken)_jwtSecurityTokenHandler.ReadToken(jwtToken.Value);

        var id = jwtSecurityToken.Claims
            .FirstOrDefault(c => c.Type == CLAIM_NAME_USER_ID)
            ?? throw new InvalidOperationException("Ошибка авторизации.");

        var email = jwtSecurityToken.Claims
            .FirstOrDefault(c => c.Type == CLAIM_NAME_EMAIL)
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
    private const string CLAIM_NAME_EMAIL = "email";
    private const string CLAIM_NAME_TYPE = "type";
    private const string CLAIM_NAME_TYPE_VALUE = "customer";
}
