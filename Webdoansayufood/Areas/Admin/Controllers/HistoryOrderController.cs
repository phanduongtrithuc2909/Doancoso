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
    public class HistoryOrderController : Controller
    {
        // GET: Admin/HistoryOrder
        
        private ApplicationDbContext _dbcontext = new ApplicationDbContext();

        public ActionResult Index(int? page)
        {
            var historyorder = _dbcontext.Orders.ToList();
            int pagesize = 3;
            int pageIndex = (page ?? 1);
            historyorder = historyorder.OrderByDescending(x => x.Id).ToList();



            return View(historyorder.ToPagedList(pageIndex, pagesize));
        }
        [HttpPost]
        public JsonResult Delete(int? id)
        {
            var items = _dbcontext.Orders.Find(id);
            if (items != null)
            {
                _dbcontext.Orders.Remove(items);
                _dbcontext.SaveChanges();
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }
        public ActionResult Detail(int id)
        {
            var orderdetail = _dbcontext.Orders.Find(id);
            return View(orderdetail);
        }
        public ActionResult Partial_Detail(int id)
        {
            var items = _dbcontext.OrderDetails.Where(x => x.Id == id).ToList();
            return PartialView(items);
        }
    }
}