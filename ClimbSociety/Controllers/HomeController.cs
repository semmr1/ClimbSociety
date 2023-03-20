using Microsoft.AspNetCore.Mvc;
using ClimbSociety.Models;
using ClimbSociety.ViewModels;
using System.Diagnostics;

namespace ClimbSociety.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private const string PageViews = "PageViews";

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            UpdatePageViewCookie();
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        
        public IActionResult GDPR()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public void UpdatePageViewCookie()
        {
            var currentCookieValue = Request.Cookies[PageViews];

            if (currentCookieValue == null)
            {
                Response.Cookies.Append(PageViews, "1");
            }
            else
            {
                var newCookieValue = short.Parse(currentCookieValue) + 1;
                Response.Cookies.Append(PageViews, newCookieValue.ToString());
            }
            if (currentCookieValue != null)
            {
                ViewData["cookies"] = int.Parse(currentCookieValue)+1;
            }
            else
            {
                ViewData["cookies"] = 1;   
            }
        }
    }
}