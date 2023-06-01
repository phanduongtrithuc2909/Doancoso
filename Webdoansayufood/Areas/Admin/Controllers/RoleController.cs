using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Webdoansayufood.Models;

namespace Webdoansayufood.Areas.Admin.Controllers
{
    public class RoleController : Controller
    {
        private ApplicationDbContext _db = new ApplicationDbContext();
        // GET: Admin/Role
        public ActionResult Index()
        {
            var item = _db.Roles.ToList();
            return View(item);
        }
        public ActionResult Create()
        {

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IdentityRole model)
        {
            if (ModelState.IsValid)
            {
                var role = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(_db));
                role.Create(model);
                return RedirectToAction("Index");

            }
            return View(model);
        }

        //public ActionResult Edit(int id)
        //{
        //    var item = _db.Roles.Find(id);
        //    return View(item);
        //}
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit(IdentityRole model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var role = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(_db));
        //        role.Update(model);
        //        return RedirectToAction("Index");

        //    }
        //    return View(model);
        //}





    }
}