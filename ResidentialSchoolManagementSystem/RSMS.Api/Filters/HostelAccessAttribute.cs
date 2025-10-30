using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

namespace RSMS.Api.Filters
{
    public class HostelAccessAttribute : Attribute, IAsyncActionFilter
    {
        private readonly bool _requireHostel;

        public HostelAccessAttribute(bool requireHostel = true)
        {
            _requireHostel = requireHostel;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var user = context.HttpContext.User;
            var role = user.FindFirst(ClaimTypes.Role)?.Value;
            var claimHostelId = user.FindFirst("RSHostelId")?.Value;
            var headerHostelId = context.HttpContext.Request.Headers["RSHostelId"].FirstOrDefault();

            Guid rSHostelId;

            // SuperAdmin
            if (role == "SuperAdmin")
            {
                if (string.IsNullOrWhiteSpace(headerHostelId) || !Guid.TryParse(headerHostelId, out rSHostelId))
                {
                    context.Result = new BadRequestObjectResult("SuperAdmin must specify a valid RSHostelId in header.");
                    return;
                }
            }
            // Users with roles other than SuperAdmin
            else
            {
                if (string.IsNullOrWhiteSpace(claimHostelId) || !Guid.TryParse(claimHostelId, out rSHostelId))
                {
                    context.Result = new UnauthorizedObjectResult("RSHostelId claim missing or invalid.");
                    return;
                }

                // (Optional) — check that header hostel matches claim if header provided
                if (!string.IsNullOrEmpty(headerHostelId) && headerHostelId != claimHostelId)
                {
                    context.Result = new ForbidResult("You are not authorized for this hostel.");
                    return;
                }
            }

            // Store in HttpContext for controller/service
            context.HttpContext.Items["RSHostelId"] = rSHostelId;

            await next();
        }
    }
}
