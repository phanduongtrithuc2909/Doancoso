using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Webdoansayufood.Models.Entity;
using Webdoansayufood.Models;

namespace Webdoansayufood.Controllers
{
    public class ProductController : Controller
    {
        private ApplicationDbContext _dbcontext = new ApplicationDbContext();        
        // GET: Product
        public ActionResult Index(string SearchString = "")
        {
            List<Product> items = _dbcontext.Products.ToList<Product>();
            if (SearchString != "")
            {
                var products = _dbcontext.Products.Where(x => x.Title.Contains(SearchString)).ToList();
                return View(products);
            }
            return View(items);
        }
        public ActionResult Detail(int id)
        {
            var detailproduct = _dbcontext.Products.Where(x => x.Id == id).FirstOrDefault();
            return View(detailproduct);
        }

    }
}