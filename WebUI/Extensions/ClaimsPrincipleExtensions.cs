using System.Security.Claims;

namespace Desk.WebUI.Extensions;

public static class ClaimsPrincipleExtensions
{
    public static Guid UserIdentifier(this ClaimsPrincipal principle, HttpContext context)
    {
        var idClaim = context.User.FindFirst(ClaimTypes.NameIdentifier);
        var userId = Guid.Parse(idClaim!.Value);

        return userId;
    }
}