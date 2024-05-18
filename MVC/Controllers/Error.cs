using Microsoft.AspNetCore.Mvc;
using MVC.Models;

namespace MVC.Controllers
{
    public class Error : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
