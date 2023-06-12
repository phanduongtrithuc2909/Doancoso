using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Webdoansayufood.Models;
using Webdoansayufood.Models.Entity;

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

        public ActionResult Checkoutt(FormCollection form)

        {
            Random random = new Random();
            ShoppingCart cart = Session["Cart"] as ShoppingCart;
            Order order = new Order();
            order.CreateDate = DateTime.Now;
            order.ModifiedDate = DateTime.Now;
            order.CustomerName = form["CustomerName"];
            order.Address = form["Address"];
            order.Email = form["Email"];
            order.Phone = form["Phone"];
            order.code = "DH" + random.Next(0,9) + random.Next(0, 9) + random.Next(0, 9) + random.Next(0, 9); 
            _db.Orders.Add(order);
            foreach (var item in cart.Items)
            {
                OrderDetail detail = new OrderDetail();
                detail.CreateDate = DateTime.Now;
                detail.ModifiedDate = DateTime.Now;
                detail.OrderId = order.Id;
                detail.ProductId = item.Shopping_Product.Id;
                detail.Quantity = item.Shopping_Quantity;
                detail.Price = item.Shopping_Product.Price;
                _db.OrderDetails.Add(detail);
            }
            _db.SaveChanges();

            // Send Mail cho khach hang
            var strSanPham = "";
            var thanhtien = decimal.Zero;
            var TongTien = decimal.Zero;
            foreach (var sp in cart.Items)
            {
                strSanPham += "<tr>";
                strSanPham += "<td>" + sp.Shopping_Product.Title + "</td>";
                strSanPham += "<td>" + sp.Shopping_Quantity + "</td>";
                strSanPham += "<td>" + sp.Shopping_Product.Price + "</td>";
                strSanPham += "<td>" + Webdoansayufood.Common.Common.FormatNumber(sp.Shopping_Total, 0) + "</td>";
                strSanPham += "</tr>";
                thanhtien += sp.Shopping_Product.Price * sp.Shopping_Quantity;
            }
            TongTien = thanhtien;
            string contentCustomer = System.IO.File.ReadAllText(Server.MapPath("~/Content/SendMail/HtmlPage2.html"));
            contentCustomer = contentCustomer.Replace("{{MaDon}}", order.code);
            contentCustomer = contentCustomer.Replace("{{SanPham}}", strSanPham);
            contentCustomer = contentCustomer.Replace("{{NgayDat}}", DateTime.Now.ToString("dd/MM/yyyy"));
            contentCustomer = contentCustomer.Replace("{{TenKhachHang}}", order.CustomerName);
            contentCustomer = contentCustomer.Replace("{{Phone}}", order.Phone);
            contentCustomer = contentCustomer.Replace("{{Email}}", order.Email);
            contentCustomer = contentCustomer.Replace("{{DiaChiNhanHang}}", order.Address);
            contentCustomer = contentCustomer.Replace("{{ThanhTien}}", Webdoansayufood.Common.Common.FormatNumber(thanhtien, 0));
            contentCustomer = contentCustomer.Replace("{{TongTien}}", Webdoansayufood.Common.Common.FormatNumber(TongTien, 0));
            Webdoansayufood.Common.Common.SendMail("ShopOnline", "Đơn hàng #" + order.code, contentCustomer.ToString(), order.Email);

            string contentAdmin = System.IO.File.ReadAllText(Server.MapPath("~/Content/SendMail/HtmlPage1.html"));
            contentAdmin = contentAdmin.Replace("{{MaDon}}", order.code);
            contentAdmin = contentAdmin.Replace("{{SanPham}}", strSanPham);
            contentAdmin = contentAdmin.Replace("{{NgayDat}}", DateTime.Now.ToString("dd/MM/yyyy"));
            contentAdmin = contentAdmin.Replace("{{TenKhachHang}}", order.CustomerName);
            contentAdmin = contentAdmin.Replace("{{Phone}}", order.Phone);
            contentAdmin = contentAdmin.Replace("{{Email}}", order.Email);
            contentAdmin = contentAdmin.Replace("{{DiaChiNhanHang}}", order.Address);
            contentAdmin = contentAdmin.Replace("{{ThanhTien}}", Webdoansayufood.Common.Common.FormatNumber(thanhtien, 0));
            contentAdmin = contentAdmin.Replace("{{TongTien}}", Webdoansayufood.Common.Common.FormatNumber(TongTien, 0));
            Webdoansayufood.Common.Common.SendMail("ShopOnline", "Đơn hàng mới #" + order.code, contentAdmin.ToString(), ConfigurationManager.AppSettings["EmailAdmin"]);




            cart.ClearCart();
            return RedirectToAction("Checkoutsucces", "ShoppingCart");
        }

    


        public ActionResult Checkoutsucces()
        {
            return View();
        }









        }

}
