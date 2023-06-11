using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Webdoansayufood.Models;

namespace Webdoansayufood.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin, Employe")]
    public class ContactController : Controller
    {
        // GET: Admin/Contact
        private ApplicationDbContext _dbcontext = new ApplicationDbContext();

        public ActionResult Index(int? page)
        {
            var contact = _dbcontext.Contacts.ToList();
            int pagesize = 3;
            int pageIndex = (page ?? 1);
            contact = contact.OrderByDescending(x => x.Id).ToList();



            return View(contact.ToPagedList(pageIndex, pagesize));
        }
        [HttpPost]
        public JsonResult Delete(int? id)
        {
            var items = _dbcontext.Contacts.Find(id);
            if (items != null)
            {
                _dbcontext.Contacts.Remove(items);
                _dbcontext.SaveChanges();
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }
    }
}