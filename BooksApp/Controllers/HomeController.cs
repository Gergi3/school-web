using Microsoft.AspNetCore.Mvc;

namespace BooksApp.Controllers;

public class HomeController : Controller
{
	public IActionResult Index()
	{
		return this.View();
	}
}
