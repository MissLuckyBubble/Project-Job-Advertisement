using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SilviqDancheva_2101321099.ActionFilters;
using SilviqDancheva_2101321099.DB;
using SilviqDancheva_2101321099.Entities;
using SilviqDancheva_2101321099.ExtentionMethods;
using SilviqDancheva_2101321099.Repositories;
using SilviqDancheva_2101321099.ViewModels.JobAds;
using SilviqDancheva_2101321099.ViewModels.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace SilviqDancheva_2101321099.Controllers
{
    [AuthFilter]
    public class JobAdController : Controller
    {

        public IActionResult Index(IndexVM model)
        {

            User loggedUser = this.HttpContext.Session.GetObject<User>("loggedUser");

            JobAdRepository repo = new JobAdRepository();
            UserToJobAdRepository UserToJobRepo = new UserToJobAdRepository();
            CategoryRepository categoryRepository = new CategoryRepository();

            model.Pager = model.Pager ?? new PagerVM();

            model.Filter ??= new FilterVM();

            model.Pager.Page = model.Pager.Page <= 0 ? 1 : model.Pager.Page;

            model.Pager.ItemsPerPage = model.Pager.ItemsPerPage <= 0 ? 10 : model.Pager.ItemsPerPage;

            Expression<Func<JobAd, bool>> filter = i => (
              (string.IsNullOrEmpty(model.Filter.Title) || i.Title.Contains(model.Filter.Title)) &&
              (string.IsNullOrEmpty(model.Filter.Description) || i.Description.Contains(model.Filter.Description)) &&
              (model.Filter.CategoryId <= 0 || model.Filter.CategoryId == i.CategoryId) &&
              (string.IsNullOrEmpty(model.Filter.Owner) || i.Owner.FirstName.Contains(model.Filter.Owner)
                                                        || i.Owner.LastName.Contains(model.Filter.Owner)
                                                        || i.Owner.Username.Contains(model.Filter.Owner)));
  

            model.Filter.ValidCategories = categoryRepository.GetAll()
                                        .Select(u => new SelectListItem()
                                        {
                                            Text = $"{u.CategoryName}",
                                            Value = u.Id.ToString(),
                                            Selected = u.Id == model.Filter.CategoryId
                                        }
                                        );

            if (loggedUser.RoleId == 1)
            {
                model.Items = repo.GetAll(i =>i.OwnerId == loggedUser.Id, filter);


                model.Pager.PagesCounter = (int)Math.Ceiling(model.Items.Count() / (double)model.Pager.ItemsPerPage);

                model.Items = repo.GetAll(i => i.OwnerId == loggedUser.Id, filter , page: model.Pager.Page, itemsPerPage: model.Pager.ItemsPerPage);

                return View(model);
            }
            else {
                model.Items = repo.GetAll(filter);

                List<JobAd> Applayed = UserToJobRepo.GetJobAd(i => i.UserId == loggedUser.Id,i => i.JobAd);

                if(Applayed != null && Applayed.Count > 0)
                {
                    foreach(JobAd item in Applayed)
                    {
                        JobAd removeItem = model.Items.Find(i => i.Id == item.Id);
                        if(removeItem != null)
                        {
                             model.Items.Remove(removeItem);
                            
                        }
                    }
                }
                model.Pager.PagesCounter = (int)Math.Ceiling(model.Items.Count() / (double)model.Pager.ItemsPerPage);

                model.Items = repo.Pager(model.Items, page: model.Pager.Page, itemsPerPage: model.Pager.ItemsPerPage);

            return View(model);
            }
        }

        [RoleFilterAttribue]
        [HttpGet]
        public IActionResult Create()
        {
            User loggedUser = this.HttpContext.Session.GetObject<User>("loggedUser");
            CreateVM model = new CreateVM();
            model.OwnerId = loggedUser.Id;

            CategoryRepository CategoryRepo = new CategoryRepository();

            List<Category> categories = new List<Category>();
            categories = CategoryRepo.GetAll(); ;

            //Assigning reolelist to ViewBag
            ViewBag.ListofCategory = categories;

            return View(model);
        }
        [RoleFilterAttribue]
        [HttpPost]
        public IActionResult Create(CreateVM model)
        {
            CategoryRepository CategoryRepo = new CategoryRepository();
            JobAdRepository adRepo = new JobAdRepository();

            User loggedUser = this.HttpContext.Session.GetObject<User>("loggedUser");

            if (model.OwnerId != loggedUser.Id)
            {
                ModelState.AddModelError("summaryError", "Owner impresonation attempt detected!");
                return View(model);
            }

            if (!ModelState.IsValid)
            {

                List<Category> categories = new List<Category>();
                categories = CategoryRepo.GetAll(); 

                //Assigning reolelist to ViewBag
                ViewBag.ListofCategory = categories;

                return View(model);
            }

            JobAd item = new JobAd();
            item.OwnerId = model.OwnerId;
            item.Title = model.Title;
            item.Description = model.Description;
            item.CategoryId = model.CategoryId;

            adRepo.Save(item);
            return RedirectToAction("Index", "JobAd");
        }
        [RoleFilterAttribue]
        [HttpGet]
        public IActionResult Edit(int id)
        {
            User loggedUser = this.HttpContext.Session.GetObject<User>("loggedUser");

            CategoryRepository CategoryRepo = new CategoryRepository();
            JobAdRepository adRepo = new JobAdRepository();

            JobAd item = adRepo.GetFirstOrDefault(p => p.Id == id);

            if (item == null)
                return RedirectToAction("Index", "JobAd");

            if (item.OwnerId != loggedUser.Id)
                return RedirectToAction("Index", "JobAd");


            List<Category> categories = new List<Category>();
            categories = CategoryRepo.GetAll();

            //Assigning reolelist to ViewBag
            ViewBag.ListofCategory = categories;

            EditVM model = new EditVM();

            model.Id = item.Id;
            model.OwnerId = item.OwnerId;
            model.Title = item.Title;
            model.Description = item.Description;
            model.CategoryId = item.CategoryId;

            return View(model);
        }
        [RoleFilterAttribue]
        [HttpPost]
        public IActionResult Edit(EditVM model)
        {
            User loggedUser = this.HttpContext.Session.GetObject<User>("loggedUser");

            CategoryRepository CategoryRepo = new CategoryRepository();

            JobAdRepository adRepo = new JobAdRepository();

            JobAd item = adRepo.GetFirstOrDefault(p => p.Id == model.Id);

            if (item.OwnerId != loggedUser.Id)
            {
                ModelState.AddModelError("summaryError", "Owner impresonation attempt detected!");
                return View(model);
            }


            item.Id = model.Id;
            item.OwnerId = model.OwnerId;
            item.Title = model.Title;
            item.Description = model.Description;
            item.CategoryId = model.CategoryId;

            adRepo.Save(item);

            return RedirectToAction("Index", "JobAd");
        }
        [RoleFilterAttribue]
        [HttpGet]
        public IActionResult Delete(int id)
        {
            JobAdRepository adRepo = new JobAdRepository();
            UserToJobAdRepository userToJobRepo = new UserToJobAdRepository();

            JobAd item = new JobAd();
            item.Id = id;

            List<UserToJobAd> jobstoDelete = userToJobRepo.GetAll(filter: j => j.JobAdId == id);
            
            adRepo.Delete(item);

            foreach(UserToJobAd i in jobstoDelete)
            {
                userToJobRepo.Delete(i);
            }

            return RedirectToAction("Index", "JobAd");
        }

        [RoleFilterAttribue]
        [HttpGet]
        public IActionResult UserApplayed(int id)
        {

            JobAdRepository adRepo = new JobAdRepository();
            UserRepository repo = new UserRepository();
            UserToJobAdRepository userToJobRepo = new UserToJobAdRepository();

            IndexVM model = new IndexVM();

            List<User> users = userToJobRepo.GetUsers(u => u.JobAdId == id, u => u.User);
                

            model.JobAd = adRepo.GetFirstOrDefault(j => j.Id == id);

            model.Users = users;

            return View(model);
        }

        [HttpGet]
        public IActionResult Applay(int Id)
        {

            UserToJobAdRepository userToJobRepo = new UserToJobAdRepository();

            User loggedUser = this.HttpContext.Session.GetObject<User>("loggedUser");

            UserToJobAd item = new UserToJobAd();
            item.JobAdId = Id;
            item.UserId = loggedUser.Id;
            userToJobRepo.Save(item);

            return RedirectToAction("Index", "JobAd");
        }
        [HttpGet]
        public IActionResult Applayed(IndexVM model)
        {
            JobAdRepository repo = new JobAdRepository();

            model.Pager = model.Pager ?? new PagerVM();

            model.Pager.Page = model.Pager.Page <= 0 ? 1 : model.Pager.Page;

            model.Pager.ItemsPerPage = model.Pager.ItemsPerPage <= 0 ? 10 : model.Pager.ItemsPerPage;

            User loggedUser = this.HttpContext.Session.GetObject<User>("loggedUser");

            UserToJobAdRepository userToJobRepo = new UserToJobAdRepository();

            List<JobAd> applayed = userToJobRepo.GetJobAd(i => i.UserId == loggedUser.Id, i => i.JobAd);

            model.Items = applayed;

            model.Pager.PagesCounter = (int)Math.Ceiling(model.Items.Count() / (double)model.Pager.ItemsPerPage);

            model.Items = repo.Pager(model.Items, page: model.Pager.Page, itemsPerPage: model.Pager.ItemsPerPage);

            return View(model);

        }

        [HttpGet]
        public IActionResult CancelApplay(int id)
        {
            UserToJobAdRepository userToJobRepo = new UserToJobAdRepository();

            User loggedUser = this.HttpContext.Session.GetObject<User>("loggedUser");

            UserToJobAd item = userToJobRepo.GetFirstOrDefault(i => i.JobAdId == id && i.UserId == loggedUser.Id);

            userToJobRepo.Delete(item);

            return RedirectToAction("Applayed", "JobAd");

        }

    }
}