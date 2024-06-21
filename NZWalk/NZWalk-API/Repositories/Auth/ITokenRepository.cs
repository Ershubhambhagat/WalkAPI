using Microsoft.AspNetCore.Identity;

namespace NZWalk_API.Repositories.Auth
{
    public interface ITokenRepository
    {
        string CreateJWTToken(IdentityUser user, List<string> Roles);
    }
}
