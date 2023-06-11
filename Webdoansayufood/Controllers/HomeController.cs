using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Webdoansayufood.Models;
using Webdoansayufood.Models.Entity;

namespace Webdoansayufood.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext _dbcontext = new ApplicationDbContext();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        public ActionResult Contact(Contact contact)
        {
            if (ModelState.IsValid)
            {
                contact.CreateDate = DateTime.Now;
                contact.ModifiedDate = DateTime.Now;
                _dbcontext.Contacts.Add(contact);
                _dbcontext.SaveChanges();
                return RedirectToAction("Contact", "Home");
            }

            return View(contact);
        }
       

    }
}