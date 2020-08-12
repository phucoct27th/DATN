using MutiShopDataContext.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MultiShop.Controllers
{
    public class SanPhamController : Controller
    {
        private DataContext db = new DataContext();
        [HttpGet]
        public ActionResult DanhSachSanPham(string loai, string danhmuc)
        {
            return View();
        }
        [HttpGet]
        public PartialViewResult ListSanPham(string loai, string danhmuc, string tuKhoa)
        {
            List<SanPham> sanPhams = new List<SanPham>();
            if(loai !=null)
            {
                int Idloai = Int32.Parse(loai);
                sanPhams = db.SanPhams.Where(s => s.IdLoaiSanPham == Idloai).ToList();
                return PartialView(sanPhams);
            }
            if(danhmuc != null)
            {
                int IdDanhMuc = Int32.Parse(danhmuc);
                sanPhams = db.SanPhams.Where(s => s.IdDanhMuc == IdDanhMuc).ToList();
                return PartialView(sanPhams);
            }
            if(tuKhoa != null)
            {
                sanPhams = db.SanPhams.Where(s => s.TenSanPham.Contains(tuKhoa)).ToList();
                return PartialView(sanPhams);
            }
            sanPhams = db.SanPhams.ToList();
            return PartialView(sanPhams);
        }
        public ActionResult ChiTiet(int? id)
        {
            if (id==null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
        public PartialViewResult ParDetails(int? id)
        {
            var _listBL = db.BinhLuans.Where(b => b.IdSanPham == id).ToList();
            ViewBag.BLuan = _listBL;
            var SanPham = db.SanPhams.Find(id.Value);
            return PartialView(SanPham);
        }
        public PartialViewResult TimKiem()
        {
            return PartialView();
        }
        public JsonResult CreateBinhLuan(BinhLuan binhLuan)
        {
            try
            {
                db.BinhLuans.Add(binhLuan);
                db.SaveChanges();
                return Json("OK",JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return Json("FALSE", JsonRequestBehavior.AllowGet);
            }
        }

    }
}