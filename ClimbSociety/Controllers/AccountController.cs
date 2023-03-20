using Microsoft.AspNetCore.Mvc;
using ClimbSociety.Models;

namespace ClimbSociety.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
            string koekje = Request.Cookies["mijncookie"];
            ViewData["koekje"] = koekje;
            string session = HttpContext.Session.GetString("pass");
            ViewData["sessie"] = session;
            return View();
        }

        public IActionResult Login()
        {
            Account accountModel = new();
            return View(accountModel);
        }

        [HttpPost]
        public IActionResult Login(Account account)
        {
            Response.Cookies.Append("mijncookie", account.Email);
            HttpContext.Session.SetString("pass", account.Password);
            return RedirectToAction("Index");
        }
    }
}
