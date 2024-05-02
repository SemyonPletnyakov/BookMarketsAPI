using Models.Autorization;

namespace BookMarketsAPI.Helpers;

public static class AuthorizationHelper
{
    public static JwtToken GetJwtTokenFromHandlers(this IHeaderDictionary headers)
    {
        var token = (headers.ContainsKey("authorization")
            ? headers["authorization"]
            : headers["Authorization"]).ToString().Replace("Bearer ", "");

        return new(token);
    }
}
