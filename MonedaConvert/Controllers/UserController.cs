using Microsoft.AspNetCore.Mvc;

namespace MonedaConvert.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
