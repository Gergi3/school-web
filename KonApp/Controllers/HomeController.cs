using Microsoft.AspNetCore.Mvc;

namespace KonApp.Controllers;
public class HomeController : Controller
{
	public IActionResult Index()
	{
		return this.View();
	}
}
