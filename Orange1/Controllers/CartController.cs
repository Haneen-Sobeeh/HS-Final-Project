using Microsoft.AspNetCore.Mvc;

namespace Orange1.Controllers
{
    public class CartController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
