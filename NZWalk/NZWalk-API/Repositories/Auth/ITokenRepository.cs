using Microsoft.AspNetCore.Identity;
using System.Runtime.InteropServices;

namespace NZWalk_API.Repositories.Auth
{
    public interface ITokenRepository
    {
        string CreateJWTToken(IdentityUser user,List<string> Roles);
    }
}
