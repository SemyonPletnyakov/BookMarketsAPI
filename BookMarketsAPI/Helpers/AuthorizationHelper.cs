using Models.Autorization;
using Models.Exceptions;

namespace BookMarketsAPI.Helpers;

public static class AuthorizationHelper
{
    public static JwtToken GetJwtTokenFromHandlers(this IHeaderDictionary headers)
    {
        if (headers.ContainsKey("authorization"))
        {
            var token = headers["authorization"].ToString().Replace("Bearer ", "");

            return new(token);
        }
        if (headers.ContainsKey("Authorization"))
        {
            var token = headers["Authorization"].ToString().Replace("Bearer ", "");

            return new(token);
        }

        throw new AuthorizationException("Нет Jwt токена.");
    }
}
