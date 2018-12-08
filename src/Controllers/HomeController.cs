using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AspCoreMVC.Models;
using Microsoft.Extensions.Configuration;

namespace AspCoreMVC.Controllers
{
    public class HomeController : Controller
    {
        public HomeController(IConfiguration Configuration)
        {
            _configuration = Configuration;
        }
        private IConfiguration _configuration;
        public IActionResult Index()
        {
            var passwords = _configuration["MySQLPassword"];
            return View();
        }

        public IActionResult Privacy()
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
