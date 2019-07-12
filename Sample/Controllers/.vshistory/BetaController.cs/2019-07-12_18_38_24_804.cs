using Microsoft.AspNetCore.Mvc;


public class BetaController : Controller
{
	public IActionResult Index()
	{
		return View();
	}
}