﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebNhaHang.Models;
using WebNhaHang.Models.EF;

namespace WebNhaHang.Controllers
{
    public class ShoppingCartController : Controller
    {
        // GET: ShoppingCart
         ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
            ShoppingCart cart = (ShoppingCart)Session["Cart"];
            if (cart != null && cart.items.Any())
            {
                ViewBag.CheckCart = cart;
            }
            return View();
        }
        public ActionResult CheckOut()
        {
            ShoppingCart cart = (ShoppingCart)Session["Cart"];
            if (cart != null&&cart.items.Any())
            {
                ViewBag.CheckCart = cart;
            }
            return View();
        }
        public ActionResult CheckOutSuccess()
        {
            
            return View();
        }
        public ActionResult Partial_item_ThanhToan()
        {
            ShoppingCart cart = (ShoppingCart)Session["Cart"];
            if (cart != null && cart.items.Any())
            {
                return PartialView(cart.items);
            }

            return PartialView();
        }
        public ActionResult Partial_item_Cart()
        {
            ShoppingCart cart = (ShoppingCart)Session["Cart"];
            if (cart != null && cart.items.Any())
            {
                return PartialView(cart.items);
            }
             
            return PartialView();
        }

        public ActionResult ShowCount()
        {
            ShoppingCart cart = (ShoppingCart)Session["Cart"];
            if (cart != null)
            {
                return Json(new { Count = cart.items.Count },JsonRequestBehavior.AllowGet); 
            }
            return Json(new { Count = 0 },JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult AddToCart(int id,int quantity)
        {
            var code = new { success = false, msg = "", code = -1 ,Count = 0};
            var db = new ApplicationDbContext();
            var checkProduct = db.Products.FirstOrDefault(x => x.id == id);
            if (checkProduct != null)
            {
                ShoppingCart cart = (ShoppingCart)Session["Cart"];
                if (cart == null)
                {
                    cart = new ShoppingCart();
                }
                ShoppingCartItem item = new ShoppingCartItem
                {
                    ProductId = checkProduct.id,
                    ProductName = checkProduct.Title,
                    CategoryName = checkProduct.ProductCategory.Title,
                    Alias=checkProduct.Alias,
                    Quantity = quantity
                };
                if (checkProduct.ProductImage.FirstOrDefault(x => x.IsDefault) != null)
                {
                    item.ProductImg = checkProduct.ProductImage.FirstOrDefault(x => x.IsDefault).Image;
                }
                item.Price = checkProduct.Price;
                if (checkProduct.PriceSale > 0)
                {
                    item.Price = (decimal)checkProduct.PriceSale;
                }
                item.TotalPrice = item.Quantity * item.Price;
                cart.AddToCart(item, quantity);
                Session["Cart"] = cart;
                code = new { success = true, msg = "Added to cart", code = 1, Count = cart.items.Count };
            }
            return Json(code);
        }

        public ActionResult Delete(int id)
        {
            var code = new { success = false, msg = "", code = -1, Count = 0 };

            ShoppingCart cart = (ShoppingCart)Session["Cart"];
            if (cart != null)
            {
                var checkProduct = cart.items.FirstOrDefault(x => x.ProductId == id);
                if (checkProduct != null)
                {
                    cart.Remove(id);
                    code = new { success = true, msg = "", code = 1, Count = cart.items.Count };
                }

            }
            return Json(code);
        }

        [HttpPost]
        public ActionResult Update(int id , int quantity)
        {
            ShoppingCart cart = (ShoppingCart)Session["Cart"];
            if (cart != null)
            {
                cart.UpdateQuantity(id,quantity);
                return Json(new { success = true });

            }
            return Json(new { success = false });
        }

        [HttpPost]
        public ActionResult DeleteAll()
        {
            ShoppingCart cart = (ShoppingCart)Session["Cart"];
            if (cart != null)
            {
                cart.ClearCart();
                return Json(new { success=true});

            }
            return Json(new { success = false });
        }

        public ActionResult Partial_CheckOut()
        {
            OrderViewModel model = new OrderViewModel();
            if (Session["FullName"]!=null)
            {
                // lấy thông tin của người dùng hiện tại
                string email = Session["Email"].ToString();
                // tạo một đối tượng DbContext để truy vấn dữ liệu
                using (var db = new ApplicationDbContext())
                {
                    // lấy thông tin của user từ database
                    var user = db.Userss.FirstOrDefault(u => u.Email == email);
                    if (user != null)
                    {
                        // gán giá trị cho thuộc tính user của biến model
                        model.user = user;
                    }
                }
            }
             
            return PartialView(model);
           
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CheckOut(OrderViewModel req)
        {
            if (ModelState.IsValid)
            {
                var cart = (ShoppingCart)Session["Cart"];
                if (cart != null && cart.items.Any())
                {
                    var order = new Order
                    {
                        CustomerName = req.CustomerName,
                        Phone = req.Phone,
                        Address = req.Address,
                        Mail = req.Mail,
                        Quantity = cart.items.Count,
                        TotalAmount = cart.items.Sum(x => x.Price * x.Quantity),
                        TypePayment = req.TypePayment,
                        CreateDate = DateTime.Now,
                        ModifieDate = DateTime.Now,
                        CreateBy = req.CustomerName,
                        Code = "DH" + new Random().Next(1000, 9999)
                    };
                    // Check product quantity and update database in one transaction
                        List<string> insufficientProducts = new List<string>();
                        int insufficientProductCount = 0;
                        using (var transaction = db.Database.BeginTransaction())
                        {
                            try
                            {
                                foreach (var item in cart.items)
                                {
                                    var product = await db.Products.FindAsync(item.ProductId);
                                    if (product == null || product.Quantity < item.Quantity)
                                    {
                                        insufficientProducts.Add(item.ProductName);
                                        insufficientProductCount++;
                                    }
                                    else
                                    {
                                        // Cập nhật số lượng sản phẩm trong cơ sở dữ liệu và lưu thông tin đơn hàng
                                        product.Quantity -= item.Quantity;
                                        order.OderDetails.Add(new OrderDetail
                                        {
                                            ProductId = item.ProductId,
                                            Quantity = item.Quantity,
                                            Price = item.Price
                                        });
                                    }
                                }

                                if (insufficientProductCount > 0)
                                {
                                    ViewBag.ProductNameList = insufficientProducts;
                                    return View("_Error");
                                }

                                db.Orders.Add(order);
                                await db.SaveChangesAsync();
                                transaction.Commit();
                            }
                            catch (Exception ex)
                            {
                                transaction.Rollback();
                                var errorMessage = "An error occurred during checkout: " + ex.Message;
                                return PartialView("_Error", errorMessage);
                            }
                        }

                    //send mail cho khachs hang
                    var strSanPham = "";
                    var thanhtien = decimal.Zero;
                    var TongTien = decimal.Zero;
                    foreach (var sp in cart.items)
                    {
                        strSanPham += "<tr>"; strSanPham += "<td>" + sp.ProductName + "</td>";
                        strSanPham += "<td>" + sp.Quantity + "</td>";
                        strSanPham += "<td>" + WebNhaHang.Common.Common.FormatNumber(sp.Price, 0) + "</td>";
                        strSanPham += "<td>" + WebNhaHang.Common.Common.FormatNumber(sp.TotalPrice, 0) + "</td>";
                        strSanPham += "</tr>"; thanhtien += sp.Price * sp.Quantity;
                    }
                    TongTien = thanhtien + 30000;
                    string contentCustomer = System.IO.File.ReadAllText(Server.MapPath("/Content/template/send7.html"));
                    contentCustomer = contentCustomer.Replace("{{MaDon}}", order.Code);
                    contentCustomer = contentCustomer.Replace("{{SanPham}}", strSanPham);
                    contentCustomer = contentCustomer.Replace("{{NgayDat}}", DateTime.Now.ToString("dd/MM/yyyy"));
                    contentCustomer = contentCustomer.Replace("{{TenKhachHang}}", order.CustomerName);
                    contentCustomer = contentCustomer.Replace("{{Phone}}", order.Phone);
                    contentCustomer = contentCustomer.Replace("{{Email}}", order.Mail);
                    contentCustomer = contentCustomer.Replace("{{DiaChiNhanHang}}", order.Address);
                    contentCustomer = contentCustomer.Replace("{{ThanhTien}}", WebNhaHang.Common.Common.FormatNumber(thanhtien, 0));
                    contentCustomer = contentCustomer.Replace("{{TongTien}}", WebNhaHang.Common.Common.FormatNumber(TongTien, 0));
                    WebNhaHang.Common.Common.SendMail("BBQ Restaurant XuoLo", "Your order #" + order.Code, contentCustomer.ToString(), order.Mail);

                    string contentAdmin = System.IO.File.ReadAllText(Server.MapPath("~/Content/template/send5.html"));
                    contentAdmin = contentAdmin.Replace("{{MaDon}}", order.Code);
                    contentAdmin = contentAdmin.Replace("{{SanPham}}", strSanPham);
                    contentAdmin = contentAdmin.Replace("{{NgayDat}}", DateTime.Now.ToString("dd/MM/yyyy"));
                    contentAdmin = contentAdmin.Replace("{{TenKhachHang}}", order.CustomerName);
                    contentAdmin = contentAdmin.Replace("{{Phone}}", order.Phone);
                    contentAdmin = contentAdmin.Replace("{{Email}}", req.Mail);
                    contentAdmin = contentAdmin.Replace("{{DiaChiNhanHang}}", order.Address);
                    contentAdmin = contentAdmin.Replace("{{ThanhTien}}", WebNhaHang.Common.Common.FormatNumber(thanhtien, 0));
                    contentAdmin = contentAdmin.Replace("{{TongTien}}", WebNhaHang.Common.Common.FormatNumber(TongTien, 0));
                    WebNhaHang.Common.Common.SendMail("BBQ Restaurant XuoLo", "New order #" + order.Code, contentAdmin.ToString(), ConfigurationManager.AppSettings["EmailAdmin"]);
                    cart.ClearCart();
                    return RedirectToAction("CheckOutSuccess");
                }
            }
            return Json(new { success = false, Code = -1 });
        }




 














      
















 
























    }























}