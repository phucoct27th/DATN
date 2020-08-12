using MutiShopDataContext.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MultiShop.Controllers
{
    public class AutenltController : Controller
    {
        DataContext db = new DataContext();
        public ActionResult DangKi()
        {
            if(Request.Cookies["infomation"] != null)
            {
                return RedirectToAction("Index", "DatHang");
            }    
            return View();
        }
        [HttpPost]
        public ActionResult DangKi(KhachHang khachHang)
        {
            try
            {
                db.KhachHangs.Add(khachHang);
                db.SaveChanges();
                return RedirectToAction("DangNhap", "Autenlt");
            }
            catch
            {
                return View();
            }
        }
        public ActionResult DangNhap()
        {
            if (Request.Cookies["infomation"] != null)
            {
                return RedirectToAction("Index", "DatHang");
            }
            return View();
        }
        [HttpPost]
        public ActionResult DangNhap(FormCollection f)
        {
            string username = f["username"].ToString();
            string password = f.Get("pass").ToString();
            if (username == "" || password == "")
            {
                ViewBag.thongbao = "nhập đầy đủ thông tin!";
                return View();
            }
            int dangnhap = this.Login(username, password);
            if (dangnhap == -2)
            {
                ViewBag.thongbao = "tài khoản không tồn tại!";
                return View();
            }
            if (dangnhap == -1)
            {
                ViewBag.thongbao = "Sai mật khẩu!";
                return View();
            }
            if (dangnhap == 1)
            {
                return RedirectToAction("Index", "DatHang");
            }
            return View();
        }
        private int Login(string username, string password)
        {
            var accout = db.KhachHangs.Where(k => k.TenDangNhap == username).SingleOrDefault();
            if (accout == null)
            {
                return -2;
            }
            else
            if (accout.MatKhau != password)
            {
                return -1;
            }
            else
            {
                SetCookie("infomation", accout.Id.ToString(), true);
                return 1;
            }
        }
        #region Cookie
        public void SetCookie(string name, string id, bool day)
        {
            int time = 0;
            if (day == true)
            {
                time = 14400;
            }
            else
            {
                time = 60;
            }
            var cookieId = new HttpCookie(name, id)
            {
                Expires = DateTime.Now.AddMinutes(time)
            };
            HttpContext.Response.Cookies.Add(cookieId);
        }
        public ActionResult DangXuat()
        {
            DeleleCookie("infomation");
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
        public void DeleleCookie(string cookieName)
        {
            if (Request.Cookies[cookieName] != null)
            {
                var c = new HttpCookie(cookieName);
                c.Expires = DateTime.Now.AddDays(-1);
                Response.Cookies.Add(c);
            }
        }
        #endregion
    }
}