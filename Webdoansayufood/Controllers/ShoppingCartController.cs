using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Webdoansayufood.Models;

namespace Webdoansayufood.Controllers
{
    public class ShoppingCartController : Controller
    {
        // GET: ShoppingCard
        // 
        ApplicationDbContext _db = new ApplicationDbContext();

        public ShoppingCart GetCart()
        {
            ShoppingCart cart = Session["Cart"] as ShoppingCart;
            if (cart == null || Session["Cart"] == null)
            {
                cart = new ShoppingCart();
                Session["Cart"] = cart;
            }
            return cart;
        }

        // gio hang
        public ActionResult Index()
        {
            if (Session["Cart"] == null)
                return RedirectToAction("Index", "ShoppingCart");
            ShoppingCart cart = Session["Cart"] as ShoppingCart;


            return View(cart);
        }
        //them vao gio hang
        public ActionResult AddToCart(int id)
        {
            var pro = _db.Products.SingleOrDefault(x => x.Id == id);
            if (pro != null)
            {
                GetCart().Add(pro);
            }
            return RedirectToAction("Index", "Product");
        }
        // gio hang




        public ActionResult Update_quantity_cart(FormCollection form)
        {
            ShoppingCart cart = Session["Cart"] as ShoppingCart;
            int id_pro = int.Parse(form["Id_product"]);
            int id_quantity = int.Parse(form["quantity"]);

            cart.Update_quantity(id_pro, id_quantity);

            return RedirectToAction("Index", "ShoppingCart");
        }


        public ActionResult XoaItemCart(int id)
        {
            ShoppingCart cart = Session["Cart"] as ShoppingCart;
            cart.Remove_CartItem(id);
            return RedirectToAction("Index", "ShoppingCart");
        }
        public ActionResult XoaAllItemCart()
        {
            ShoppingCart cart = Session["Cart"] as ShoppingCart;
            cart.ClearCart();
            return RedirectToAction("Index", "ShoppingCart");
        }


        //so luong san pham trong gio hang
        public PartialViewResult Bagcart()
        {
            int _item = 0;
            ShoppingCart cart = Session["Cart"] as ShoppingCart;
            if (cart != null)
            {
                _item = cart.Total_Quantity();
            }

            ViewBag.infoCart = _item;
            return PartialView("Bagcart");
        }

        // thanh toan

        public ActionResult Checkout()
        {
            if (Session["Cart"] == null)
                return RedirectToAction("Index", "ShoppingCart");
            ShoppingCart cart = Session["Cart"] as ShoppingCart;


            return View(cart);

        }
        //public ActionResult Checkoutsuccess(OrderViewModel )
        //{
        //    if (Session["Cart"] == null)
        //        return RedirectToAction("Index", "ShoppingCart");
        //    ShoppingCart cart = Session["Cart"] as ShoppingCart;


        //    return View(cart);

        //}









    }
}