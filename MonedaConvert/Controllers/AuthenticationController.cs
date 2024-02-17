using Microsoft.AspNetCore.Mvc;

namespace MonedaConvert.Controllers
{
    public class AuthenticationController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
