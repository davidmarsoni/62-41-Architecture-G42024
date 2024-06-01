using Microsoft.AspNetCore.Mvc;
using MVC.Models;
using MVC.Services.Interfaces;
using System.Diagnostics;

namespace MVC.Controllers
{
    public class LoginController : Controller
    {
        private readonly ILogger<LoginController> _logger;

        public LoginController(ILogger<LoginController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
