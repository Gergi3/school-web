using Microsoft.AspNetCore.Mvc.Filters;

namespace BooksApp.Attributes;

[AttributeUsage(
	AttributeTargets.Method | AttributeTargets.Class,
	Inherited = false,
	AllowMultiple = false
)]
internal class OnlyAnonymousAttribute : Attribute, IAuthorizationFilter
{
	public void OnAuthorization(AuthorizationFilterContext context)
	{
		var user = context.HttpContext.User;
		if (user != null
			&& user.Identity != null
			&& user.Identity.IsAuthenticated
		)
		{
			context.HttpContext.Response.StatusCode = 302;
			context.HttpContext.Response.Redirect("/home/");
		}
	}
}
