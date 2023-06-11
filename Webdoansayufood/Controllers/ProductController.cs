using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Webdoansayufood.Models.Entity;
using Webdoansayufood.Models;
using PagedList;

namespace Webdoansayufood.Controllers
{
    public class ProductController : Controller
    {
        private ApplicationDbContext _dbcontext = new ApplicationDbContext();        
        // GET: Product
        public ActionResult Index(string current,string SearchString, int? page)
        {
            var items = new List<Product>();
            if (SearchString != null)
            {
                page = 1;
            }
            else
            {
                SearchString = current;
            }
            if (!string.IsNullOrEmpty(SearchString))
            {
                 items = _dbcontext.Products.Where(x => x.Title.Contains(SearchString)).ToList();
            }
            else
            {
                items = _dbcontext.Products.ToList();
            }
            ViewBag.CurrentFilter = SearchString;
            int pagesize = 8;
            int pageIndex = (page ?? 1);
            items = items.OrderByDescending(x => x.Id).ToList();
            return View(items.ToPagedList(pageIndex, pagesize));
        }
        public ActionResult Detail(int id)
        {
            var detailproduct = _dbcontext.Products.Where(x => x.Id == id).FirstOrDefault();
            return View(detailproduct);
        }

        


    }
}