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
    public class HomeController : Controller
    {
        private ApplicationDbContext _dbcontext = new ApplicationDbContext();
        // GET: Admin/Home
        public ActionResult Index(int? page)
        {
            var historyorder = _dbcontext.Orders.ToList();
            int pagesize = 3;
            int pageIndex = (page ?? 1);
            historyorder = historyorder.OrderByDescending(x => x.Id).ToList();



            return View(historyorder.ToPagedList(pageIndex, pagesize));
        }


    }
}