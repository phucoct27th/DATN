using MutiShopDataContext.Heper;
using MutiShopDataContext.Model;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
namespace MultiShop.Controllers
{
    public class DatHangController : Controller
    {
        DataContext db = new DataContext();
        public ActionResult Index(int? page)
        {
            List<SanPham> listdat = new List<SanPham>();
            if (Session["bookingproduct"] == null)
            {
                return RedirectToAction("DanhSachSanPham", "SanPham");
            }
            List<GioHang> _listDatHang = this.LayDatGioHang();
            foreach (var item in _listDatHang)
            {
                listdat.Add(db.SanPhams.Find(item.IdSanPham));
            }
            return View(listdat);
        }
        [HttpGet]
        public JsonResult DanhSachDat()
        {
            List<JsonGioHang> listdat = new List<JsonGioHang>();
            if (Session["bookingproduct"] == null)
            {
                Response.StatusCode = 400;
            }
            List<GioHang> _listDatHang = this.LayDatGioHang();
            foreach (var item in _listDatHang)
            {
                SanPham sanPham = db.SanPhams.Find(item.IdSanPham);
                JsonGioHang jsonGioHang = new JsonGioHang(sanPham.Id, sanPham.TenSanPham, sanPham.Anh, sanPham.Gia.Value);
                listdat.Add(jsonGioHang);
            }
            return Json(listdat,JsonRequestBehavior.AllowGet);
        }
        public List<GioHang> LayDatGioHang()
        {
            List<GioHang> lstDatThue = Session["bookingproduct"] as List<GioHang>;
            if (lstDatThue == null)
            {
                lstDatThue = new List<GioHang>();
                Session["bookingproduct"] = lstDatThue;
            }
            return lstDatThue;
        }
        [HttpPost]
        public JsonResult ThemDatHang(int IdSanPham)
        {
            SanPham tour = db.SanPhams.SingleOrDefault(n => n.Id == IdSanPham);
            if (tour == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            List<GioHang> _ListGioHang = this.LayDatGioHang();
            GioHang sp = _ListGioHang.Find(n => n.IdSanPham == IdSanPham);
            if (sp == null)
            {
                sp = new GioHang(IdSanPham);
                _ListGioHang.Add(sp);
                return Json("OK", JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json("Failse", JsonRequestBehavior.AllowGet);
            }
        }
        public ActionResult XoaDathang(int Id)
        {
            SanPham _sp = db.SanPhams.SingleOrDefault(n => n.Id == Id);
            if (_sp == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            List<GioHang> _listGiohang = LayDatGioHang();
            GioHang dt = _listGiohang.SingleOrDefault(n => n.IdSanPham == Id);
            if (dt != null)
            {
                _listGiohang.RemoveAll(n => n.IdSanPham == Id);
            }
            if (_listGiohang.Count == 0)
            {
                return RedirectToAction("Index", "DatHang");
            }
            return RedirectToAction("Index", "DatHang");
        }
        [HttpGet]
        public JsonResult DeleteGioHang(int id)
        {
            SanPham _sp = db.SanPhams.SingleOrDefault(n => n.Id == id);
            if (_sp == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            List<GioHang> _listGiohang = LayDatGioHang();
            GioHang dt = _listGiohang.SingleOrDefault(n => n.IdSanPham == id);
            if (dt != null)
            {
                _listGiohang.RemoveAll(n => n.IdSanPham == id);
            }
            if (_listGiohang.Count == 0)
            {
                return Json("OK", JsonRequestBehavior.AllowGet);
            }
            return Json("OK", JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public  JsonResult  XacNhanDatHang(DatHang datHang)
        {
            try
            {
                db.DatHangs.Add(datHang);
                db.SaveChanges();
                return Json("OK", JsonRequestBehavior.AllowGet);

            }
            catch
            {
                return Json("Failse", JsonRequestBehavior.AllowGet);

            }
        }
       
    }
}