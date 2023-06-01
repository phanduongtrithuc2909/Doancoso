using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Webdoansayufood.Models.Entity;
using Webdoansayufood.Models;

namespace Webdoansayufood.Areas.Admin.Controllers
{
    public class ProductController : Controller
    {
        private ApplicationDbContext _dbcontext = new ApplicationDbContext();
        // GET: Admin/Product
        public ProductController()
        {
            _dbcontext = new ApplicationDbContext();
        }
        public ActionResult Index()
        {
            var products = _dbcontext.Products;
            return View(products);
        }
        public ActionResult Add()
        {
            ViewBag.Category = new SelectList(_dbcontext.Categories.ToList(), "Id", "Title");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(Product model)
        {
            if (ModelState.IsValid)
            {

                model.CreateDate = DateTime.Now;
                model.ModifiedDate = DateTime.Now;
                _dbcontext.Products.Add(model);
                _dbcontext.SaveChanges();
                return RedirectToAction("Index");

            }

            return View(model);

        }
        //public ActionResult Search(string SearchString)
        //{
        //    var categorys = _dbcontext.Products.Where(x => x.Category.Contains(SearchString)).ToList();

        //    return View(categorys);
        //}
        public ActionResult Edit(int id)
        {
            ViewBag.Category = new SelectList(_dbcontext.Categories.ToList(), "Id", "Title");
            var items = _dbcontext.Products.Find(id);
            return View(items);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Product model)
        {
            if (ModelState.IsValid)
            {
                model.ModifiedDate = DateTime.Now;
                _dbcontext.Products.Attach(model);
                _dbcontext.Entry(model).State = System.Data.Entity.EntityState.Modified;
                _dbcontext.SaveChanges();
                return RedirectToAction("Index");

            }

            return View(model);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Delete(int? id)
        {
            var items = _dbcontext.Products.Find(id);
            if (items != null)
            {
                _dbcontext.Products.Remove(items);
                _dbcontext.SaveChanges();
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }


    }
}