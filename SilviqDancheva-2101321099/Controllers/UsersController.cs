using Microsoft.AspNetCore.Mvc;
using SilviqDancheva_2101321099.ActionFilters;
using SilviqDancheva_2101321099.DB;
using SilviqDancheva_2101321099.Entities;
using SilviqDancheva_2101321099.ExtentionMethods;
using SilviqDancheva_2101321099.Repositories;
using SilviqDancheva_2101321099.ViewModels.Users;
using System.Collections.Generic;
using System.Linq;

namespace SilviqDancheva_2101321099.Controllers
{
    public class UsersController : Controller
    {
        [AuthFilter]
        [HttpGet]
        public IActionResult Profile(int id)
        {
            User loggedUser = this.HttpContext.Session.GetObject<User>("loggedUser");

            UserRepository repo = new UserRepository();

            ProfileVM model = new ProfileVM();

            model.User = repo.GetFirstOrDefault(i => i.Id == id);

            if (loggedUser == null)
            {
                return RedirectToAction("Index", "Home");
            }
            if(id != loggedUser.Id)
            {
                return RedirectToAction("Index", "Home");
            }
            else return View(model);
        }
        [HttpGet]
        public IActionResult Register()
        {
            //geting data
            RoleRepository roleRepo = new RoleRepository();
            List<Role> roleList = new List<Role>();

            roleList = roleRepo.GetAll();

            //Assigning reolelist to ViewBag
            ViewBag.ListofRole = roleList;

            RegisterVM model = new RegisterVM();

            return View(model);
        }
        [HttpPost]
        public IActionResult Register(RegisterVM model)
        {
            UserRepository repo = new UserRepository();

            if (!ModelState.IsValid)
            {
                RoleRepository roleRepo = new RoleRepository();
                List<Role> roleList = new List<Role>();
                roleList = roleRepo.GetAll();

                //Assigning reolelist to ViewBag
                ViewBag.ListofRole = roleList;

                return View(model);
            }

            //do input validation
            User item = new User();
            item.Username = model.Username;
            item.Password = model.Password;
            item.FirstName = model.FirstName;
            item.LastName = model.LastName;
            item.RoleId = model.RoleId;

          

            //add User
            repo.Save(item);

            return RedirectToAction("Login", "Home");
        }
        [AuthFilter]
        [HttpGet]
        public IActionResult Delete(int id)
        {
            UserRepository repo = new UserRepository();
            UserToJobAdRepository userToJobRepo = new UserToJobAdRepository();

            User item = new User();
            item.Id = id;

            List<UserToJobAd> userToDelete = userToJobRepo.GetAll(u => u.UserId == id);

            repo.Delete(item);

            foreach (UserToJobAd i in userToDelete)
            {
                userToJobRepo.Delete(i);
            }

            HttpContext.Session.Remove("loggedUser");

            return RedirectToAction("Index", "Home");
        }
        [AuthFilter]
        [HttpGet]
        public IActionResult Edit(int id)
        {
            UserRepository repo = new UserRepository();

            User item = repo.GetFirstOrDefault(u => u.Id == id);

            if (item == null)
            {
                return RedirectToAction("Profile", "Users");
            }

            EditVM model = new EditVM();
            model.Id = item.Id;
            model.Username = item.Username;
            model.Password = item.Password;
            model.FirstName = item.FirstName;
            model.LastName = item.LastName;
            model.RoleId = item.RoleId;

            return View(model);
        }
        [AuthFilter]
        [HttpPost]
        public IActionResult Edit(EditVM model)
        {
            if (!ModelState.IsValid)
                return View(model);

            //do input validation
            User item = new User();
            item.Id = model.Id;
            item.Username = model.Username;
            item.Password = model.Password;
            item.FirstName = model.FirstName;
            item.LastName = model.LastName;
            item.RoleId = model.RoleId;

            //edit User
            UserRepository repo = new UserRepository();
            repo.Save(item);

            HttpContext.Session.SetObject("loggedUser", item);

            return RedirectToAction("Profile", "Users");
        }

    }
}
