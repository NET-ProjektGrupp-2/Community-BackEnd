using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Community_BackEnd.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class AdminstrationController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
