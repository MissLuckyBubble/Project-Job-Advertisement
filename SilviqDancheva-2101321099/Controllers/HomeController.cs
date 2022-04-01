using Microsoft.AspNetCore.Mvc;
using SilviqDancheva_2101321099.ActionFilters;
using SilviqDancheva_2101321099.Entities;
using SilviqDancheva_2101321099.ExtentionMethods;
using SilviqDancheva_2101321099.Repositories;
using SilviqDancheva_2101321099.ViewModels.Home;
using System.Linq;

namespace SilviqDancheva_2101321099.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(LoginVM model)
        {
            if (!this.ModelState.IsValid)
            {
                return View(model);
            }

            UserRepository repo = new UserRepository();

            User loggedUser = repo.GetFirstOrDefault(u => u.Username == model.Username && u.Password == model.Password);

            if (loggedUser == null)
            {
                this.ModelState.AddModelError("authError", "Invalid username or password!");
                return View(model);
            }

            HttpContext.Session.SetObject("loggedUser", loggedUser);

            return RedirectToAction("Index", "Home");
        }
        [AuthFilter]
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("loggedUser");
            return RedirectToAction("Index", "Home");
        }
    }
}
