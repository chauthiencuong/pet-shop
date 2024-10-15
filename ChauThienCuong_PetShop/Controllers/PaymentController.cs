using ChauThienCuong_PetShop.Context;
using ChauThienCuong_PetShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ChauThienCuong_PetShop.Controllers
{
    public class PaymentController : Controller
    {
        // GET: Payment
        PetShopEntities obj = new PetShopEntities();
        // GET: Payment
        public ActionResult Index()
        {
            if (Session["idUser"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            else
            {
                var lstCart = (List<CartModel>)Session["cart"];
                Order objOrder = new Order();
                objOrder.Name = "Donhang-" + DateTime.Now.ToString("yyyyMMddHHmmss");
                objOrder.UserId = int.Parse(Session["idUser"].ToString());
                objOrder.CreatedOnUtc = DateTime.Now;
                objOrder.Status = 1;
                obj.Orders.Add(objOrder);
                obj.SaveChanges();

                int intOrderId = objOrder.Id;

                List<OrderDetail> lstOrderDetail = new List<OrderDetail>();

                foreach (var item in lstCart)
                {
                    OrderDetail objj = new OrderDetail();
                    objj.Quantity = item.Quantity;
                    objj.OrderId = intOrderId;
                    objj.ProductId = item.Product.id;
                    lstOrderDetail.Add(objj);
                }
                obj.OrderDetails.AddRange(lstOrderDetail);
                obj.SaveChanges();
            }
            return View();
        }
    }
}