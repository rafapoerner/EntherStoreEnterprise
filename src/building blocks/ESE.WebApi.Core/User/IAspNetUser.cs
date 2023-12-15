using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace ESE.WebApi.Core.User
{
    public interface IAspNetUser
    {
        string Name { get; }
        Guid GetUserId();
        string GetUserEmail();
        string GetUserToken();
        bool IsAuthenticated();
        bool HasRole(string role);

        IEnumerable<Claim> ObterClaims();
        HttpContext ObterHttpContext();
    }
}
