using MultiShop.Common;
using MutiShopDataContext.Heper;
using MutiShopDataContext.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.Security;

namespace MultiShop.Areas.admin.Controllers
{
    public class AutentionController : Controller
    {
        private DataContext db = new DataContext();
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(FormCollection form)
        {
            string tendangnhap = form["usermane"].ToString();
            string matkhau = form.Get("password").ToString();
            if(CheckLogin(tendangnhap, matkhau) == -2)
            {
                ViewBag.thongbao = "Không được để trống !";
                return View();
            }
            if (CheckLogin(tendangnhap, matkhau) == -1)
            {
                ViewBag.thongbao = "Tên đăng nhập không tồn tại !";
                return View();
            }
            if (CheckLogin(tendangnhap, matkhau) == -0)
            {
                ViewBag.thongbao = "Sai mật khẩu !";
                return View();
            }

            return RedirectToAction("Index","Home");
        }

        private int CheckLogin(string username, string password)
        {
            
            if(username =="" || password =="")
            {
                return -2;
            }
            var user = db.QuanTris.Where(q => q.TenDangNhap == username).SingleOrDefault();
            if(user==null)
            {
                return -1;
            }
            if (user.MatKhau != password)
            {
                return 0;
            }
            else
            {
                var phanquyen = db.PhanQuyens.Where(p => p.IdQuanTri == user.Id).ToList();
                List<PhanQuyenAdmin> listphanquyen = new List<PhanQuyenAdmin>();
                foreach (var item in phanquyen)
                {
                    PhanQuyenAdmin phanQuyenAdmin = new PhanQuyenAdmin();
                    phanQuyenAdmin.IdQuyen = item.IdQuyen.ToString();
                    listphanquyen.Add(phanQuyenAdmin);
                }
                CookieAdmin cookieAdmin = new CookieAdmin();
                cookieAdmin.Id = user.Id.ToString();
                cookieAdmin.Phanquyens = listphanquyen;
                SetCookie("autoiadmin", cookieAdmin, true);
                return 1;
            }
        }
        public ActionResult Logout()
        {
            DeleleCookie("autoiadmin");
            DeleleCookie("adminstractor");
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }
        public void SetCookie(string name, CookieAdmin cookieAdmin, bool day)
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
            var cookieId = new HttpCookie("adminstractor", cookieAdmin.Id.ToString())
            {
                Expires = DateTime.Now.AddMinutes(time)
            };
            string myObjectJson = new JavaScriptSerializer().Serialize(cookieAdmin);
            var cookie = new HttpCookie(name, myObjectJson)
            {
                Expires = DateTime.Now.AddMinutes(time)
            };
            HttpContext.Response.Cookies.Add(cookie);
            HttpContext.Response.Cookies.Add(cookieId);
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
    }
}