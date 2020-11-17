using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;
using Project_Sem3.Models;
using Model.DataModel;

namespace Project_Sem3.Controllers
{
    public class ShopController : Controller
    {
        ProjectDbContext db = new ProjectDbContext();
        // GET: Shop
        public ActionResult Index(string cateid, int? page, string authorid, string publisherid, string search)
        {
            int pageSize = 3;
            int pageNumber = page ?? 1;
            //  Gán biến bằng danh sách sản phẩm 
            var data = db.Products.ToList();

            //điều kiện thể loại
            if (!string.IsNullOrEmpty(search))
            {
                data = data.Where(x => x.pro_name.ToLower().Contains(search.ToLower())).OrderByDescending(x => x.pro_id).ToList();

            }
            ViewBag.search = search;
            if (cateid != null)
            {
                data = data.Where(x => x.cat_id == cateid).ToList();

            }
            ViewBag.cata = cateid;
            //điều kiện tác giả
            if (authorid != null)
            {
                data = data.Where(x => x.author_id == authorid).ToList();
            }
            ViewBag.au = authorid;
            //điều kiện nxb

            if (publisherid != null)
            {
                data = data.Where(x => x.pub_id == publisherid).ToList();
            }
            ViewBag.pu = publisherid;

            //Page vẫn lỗi 

            return View(data.OrderByDescending(x => x.pro_id).ToPagedList(pageNumber, pageSize));
        }

        public ActionResult AddToBasket(string id)
        {
            //xử lý đưa sản vào giỏ hàng
            List<Basket> bk = new List<Basket>();
            if (Session["basket"] != null)
            {
                bk = (List<Basket>)Session["basket"];
                var checkexist = bk.SingleOrDefault(x => x.ProductId == id);
                if (checkexist != null)
                {
                    checkexist.Quantity++;
                    checkexist.Money = checkexist.Price * checkexist.Quantity;

                    Session["basket"] = bk;

                    //show giỏ hàng
                    return RedirectToAction("ShowBasket");

                }
                else
                {
                    //Tìm sản phẩm có id
                    var b = db.Products.Find(id);
                    if (b != null)
                    {
                        Basket basket = new Basket
                        {
                            ProductId = b.pro_id,
                            ProductName = b.pro_name,
                            Price = b.pro_sale_price,
                            Quantity = 1,
                            Picture = b.pro_image,
                            Money = b.pro_sale_price,
                        };
                        bk.Add(basket);
                    }
                    else
                    {
                        return HttpNotFound();
                    }
                    Session["basket"] = bk;
                }

                //show giỏ hàng

            }
            else
            {
                //Tìm sản phẩm có id
                var b = db.Products.Find(id);
                if (b != null)
                {
                    Basket basket = new Basket
                    {
                        ProductId = b.pro_id,
                        ProductName = b.pro_name,
                        Price = b.pro_sale_price,
                        Quantity = 1,
                        Picture = b.pro_image,
                        Money = b.pro_sale_price,
                    };
                    bk.Add(basket);
                   
                }
                else
                {
                    return HttpNotFound();
                }
                Session["basket"] = bk;

            }
            return RedirectToAction("ShowBasket");
        }

        public PartialViewResult ShowBasket()
        {
            List<Basket> baskets = (List<Basket>)Session["basket"];

            ViewBag.Total = baskets != null ? baskets.Sum(x => x.Price * x.Quantity) : 0;
            return PartialView(baskets);
        }
        public ActionResult Delete(string id)
        {
            List<Basket> basket = Session["basket"] as List<Basket>;
            Basket xoa = basket.FirstOrDefault(x => x.ProductId == id);
            if (xoa != null)
            {
                basket.Remove(xoa);
            }
            return RedirectToAction("ShowBasket");
        }
        public RedirectToRouteResult UpdateCart(string Id, int? quantity)
        {
            List<Basket> basket = Session["basket"] as List<Basket>;
            Basket baskets = basket.FirstOrDefault(x => x.ProductId == Id);
            if (baskets != null)
            {
                baskets.Quantity = quantity.Value;
                baskets.Money = baskets.Price * baskets.Quantity;
            }

            Session["basket"] = basket;
            return RedirectToAction("ShowBasket");



        }

        public PartialViewResult CheckOut()
        {
            List<Basket> baskets = (List<Basket>)Session["basket"];
            if (baskets != null)
            {
                ViewBag.Total = baskets.Sum(x => x.Price * x.Quantity);
            }           
            return PartialView(baskets);
        }
        public ActionResult CheckOutt()
        {
            var users = Session["admin"];
            Customer user = db.Customers.Find(users);
            ViewBag.us = user;
            return View(users);
        }

        [HttpPost]
        public ActionResult CheckOutt( string payment_met, string fullname, string address, string phone, Oders order , string descripsion , string date)
        {
            List<Basket> carts = (List<Basket>)Session["basket"];
            var total = carts.Sum(x => x.Price * x.Quantity);
            var uid = Session["admin"].ToString();
            Customer user = db.Customers.Find(uid);
            var bill = new Oders();
            bill.Oders_id = "OD" + DateTime.Now.ToString("ddMMyyyy-hhmmss");
            bill.cus_id = user.cus_id;
            bill.fullname = fullname;
            bill.phone = phone;
            bill.address = address;
            bill.Description = descripsion;
            bill.total_amount = total;
            bill.payment_method = payment_met;
            bill.create_date = DateTime.Now;
            try
            {
                db.Oderss.Add(bill);
                db.SaveChanges();

                var listPay = (List<Basket>)Session["basket"];
                foreach (var item in listPay)
                {
                    var detail = new Oders_Detail();
                    //detail.OrderDetailId = "DT" + DateTime.Now.ToString("dd/MM/yyyy-mmhhss");
                    detail.Oders_id = bill.Oders_id;
                    detail.pro_id = item.ProductId;
                    detail.quantity = item.Quantity;
                    detail.detail_sale_price = item.Price;
                    detail.total_price = item.Money;
                    db.Oders_Details.Add(detail);
                    db.SaveChanges();
                }
                Session.Remove("basket");
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Có lỗi xảy ra không thể thực hiện thanh toán: ", ex);
            }
            return View("Success");
        }

        public ActionResult Success()
        {
            return View();
        }



    }
}
