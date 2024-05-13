using Microsoft.AspNetCore.Identity;

namespace BaiThucHanhWeb.Repositories
{
    public interface ITokenRepository
    {
        string CreateJWTToken(IdentityUser user, List<string> roles);
    }
}
