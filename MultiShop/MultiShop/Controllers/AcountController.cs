using MutiShopDataContext.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MultiShop.Controllers
{
    public class AcountController : Controller
    {
        DataContext db = new DataContext();
        public ActionResult Index()
        {
            var Id = Request.Cookies["infomation"];
            if(Id==null)
            {
                return RedirectToAction("Autenlt", "DangNhap");
            }
            int IdKhachHang = Int32.Parse(Id.Value);
            var Acount = db.KhachHangs.Find(IdKhachHang);
            var _listDat = db.DatHangs.Where(d=>d.NguoiDat == IdKhachHang);
            ViewBag.dathang = _listDat.ToList();
            return View(Acount);
        }
        [HttpPost]
        public ActionResult UpdateInfo(KhachHang khachHang)
        {
            db.Entry(khachHang).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index", "Acount");
        }
    }
    
}