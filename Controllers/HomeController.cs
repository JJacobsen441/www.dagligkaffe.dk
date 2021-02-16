using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using www.dagligkaffe.dk.Common;
using www.dagligkaffe.dk.Models;

namespace www.dagligkaffe.dk.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IHttpContextAccessor httpcon;
        private ErrorHandler err = new ErrorHandler();
        private Statistics stats = new Statistics();
        private Access access;
        private Session session;
        public HomeController(ILogger<HomeController> logger)
        {
            httpcon = new HttpContextAccessor();
            session = new Session(httpcon);
            string guid = Guid.NewGuid().ToString();
            access = new Access("entry", guid);
            _logger = logger;
        }
        
        public IActionResult Index()
        {
            //ViewBag.Session = session;
            return RedirectToAction("Index", "Coffee");
            //return View();
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
