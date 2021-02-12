using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;

/*
 * HomeController.cs
 * Controller fuer das Frontend, fuer alle Routen, welche nicht mit /api/ starten
 * Autor: Dominik Strutz
 */

namespace HowWiki.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Error()
        {
            ViewData["RequestId"] = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
            return View();
        }
    }
}
