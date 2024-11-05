using System.Security.Claims;

namespace Desk.WebUI.Extensions;

public static class HttpContextExtensions
{
    public static Guid UserIdentifier(this HttpContext context)
    {
        var idClaim = context.User.FindFirst(ClaimTypes.NameIdentifier);
        var userId = Guid.Parse(idClaim!.Value);

        return userId;
    }
}