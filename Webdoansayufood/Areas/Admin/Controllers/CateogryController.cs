using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Webdoansayufood.Models.Entity;
using Webdoansayufood.Models;

namespace Webdoansayufood.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin, Employe")]
    public class CategoryController : Controller
    {
        private ApplicationDbContext _dbcontext = new ApplicationDbContext();
        // GET: Admin/Category
        public ActionResult Index()
        {
            var categorys = _dbcontext.Categories;
            return View(categorys);
        }
        public ActionResult Add()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(Category model)
        {
            if (ModelState.IsValid)
            {

                _dbcontext.Categories.Add(model);
                _dbcontext.SaveChanges();
                return RedirectToAction("Index");

            }

            return View(model);

        }
        public ActionResult Edit(int id)
        {
            var items = _dbcontext.Categories.Find(id);
            return View(items);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Category model)
        {
            if (ModelState.IsValid)
            {
                _dbcontext.Categories.Attach(model);
                _dbcontext.Entry(model).State = System.Data.Entity.EntityState.Modified;
                _dbcontext.SaveChanges();
                return RedirectToAction("Index");

            }

            return View(model);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            var items = _dbcontext.Categories.Find(id);
            if (items != null)
            {
                _dbcontext.Categories.Remove(items);
                _dbcontext.SaveChanges();
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }


    }
}