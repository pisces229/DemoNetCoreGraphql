using Microsoft.AspNetCore.Mvc;

namespace DemoNetCoreGraphql.Graphiql.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
