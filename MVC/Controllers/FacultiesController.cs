using Microsoft.AspNetCore.Mvc;
using MVC.Models;
using MVC.Services.Interfaces;
using MVCProject.Controllers;
using System.Diagnostics;

namespace MVC.Controllers
{
    public class FacultiesController : Controller
    {
        private readonly ILogger<FacultiesController> _logger;
        private readonly IAccountService _accountService;

        public FacultiesController(ILogger<FacultiesController> logger, IAccountService accountService)
        {
            _logger = logger;
            _accountService = accountService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
