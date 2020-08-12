
using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MultiShop.Common;
using MutiShopDataContext.Model;
using PagedList;

namespace MultiShop.Areas.admin.Controllers
{
    [HasCredential(RoleID = "3")]
    public class SanPhamsController : Controller
    {
        private DataContext db = new DataContext();

        // GET: admin/SanPhams
        public ActionResult Index(int? page)
        {
            int _pageSize = 20;
            int pageNumber = page ?? 1;
            var _listdanhmuc = db.SanPhams.Include(s => s.DanhMuc).Include(s => s.LoaiSanPham).Include(s => s.QuanTri).Include(s => s.QuanTri1).ToList();
            return View(_listdanhmuc.ToPagedList(pageNumber, _pageSize));
        }
        /*public ActionResult Index()
        {
            var sanPhams = db.SanPhams.Include(s => s.DanhMuc).Include(s => s.LoaiSanPham).Include(s => s.QuanTri).Include(s => s.QuanTri1);
            return View(sanPhams.ToList());
        }*/

        // GET: admin/SanPhams/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SanPham sanPham = db.SanPhams.Find(id);
            if (sanPham == null)
            {
                return HttpNotFound();
            }
            return View(sanPham);
        }

        // GET: admin/SanPhams/Create
        public ActionResult Create()
        {
            ViewBag.IdDanhMuc = new SelectList(db.DanhMucs, "Id", "TenDanhMuc");
            ViewBag.IdLoaiSanPham = new SelectList(db.LoaiSanPhams, "Id", "TenLoai");
            ViewBag.NguoiSua = new SelectList(db.QuanTris, "Id", "HoVaTen");
            ViewBag.NguoiThem = new SelectList(db.QuanTris, "Id", "HoVaTen");
            return View();
        }

        // POST: admin/SanPhams/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,TenSanPham,IdDanhMuc,IdLoaiSanPham,MauSac,Size,Anh,Mota,TrangThai,NoiDung,NgayThem,NguoiThem,NgaySua,NguoiSua,Gia,LuotXem")] SanPham sanPham, HttpPostedFileBase FileAnh)
        {
            if (ModelState.IsValid)
            {
                sanPham.Anh = SaveAnh(FileAnh);
                sanPham.NguoiThem = 2;
                sanPham.NgayThem = DateTime.Now;
                sanPham.LuotXem = 0;
                db.SanPhams.Add(sanPham);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdDanhMuc = new SelectList(db.DanhMucs, "Id", "TenDanhMuc", sanPham.IdDanhMuc);
            ViewBag.IdLoaiSanPham = new SelectList(db.LoaiSanPhams, "Id", "TenLoai", sanPham.IdLoaiSanPham);
            ViewBag.NguoiSua = new SelectList(db.QuanTris, "Id", "HoVaTen", sanPham.NguoiSua);
            ViewBag.NguoiThem = new SelectList(db.QuanTris, "Id", "HoVaTen", sanPham.NguoiThem);
            return View(sanPham);
        }

        // GET: admin/SanPhams/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SanPham sanPham = db.SanPhams.Find(id);
            if (sanPham == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdDanhMuc = new SelectList(db.DanhMucs, "Id", "TenDanhMuc", sanPham.IdDanhMuc);
            ViewBag.IdLoaiSanPham = new SelectList(db.LoaiSanPhams, "Id", "TenLoai", sanPham.IdLoaiSanPham);
            ViewBag.NguoiSua = new SelectList(db.QuanTris, "Id", "HoVaTen", sanPham.NguoiSua);
            ViewBag.NguoiThem = new SelectList(db.QuanTris, "Id", "HoVaTen", sanPham.NguoiThem);
            return View(sanPham);
        }

        // POST: admin/SanPhams/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,TenSanPham,IdDanhMuc,IdLoaiSanPham,MauSac,Size,Anh,Mota,TrangThai,NoiDung,NgayThem,NguoiThem,NgaySua,NguoiSua,Gia,LuotXem")] SanPham sanPham, HttpPostedFileBase FileAnh)
        {
            if (ModelState.IsValid)
            {
                if(FileAnh != null)
                {
                    sanPham.Anh = SaveAnh(FileAnh);
                }
                sanPham.NgaySua = DateTime.Now;
                sanPham.NguoiSua = 2;
                db.Entry(sanPham).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdDanhMuc = new SelectList(db.DanhMucs, "Id", "TenDanhMuc", sanPham.IdDanhMuc);
            ViewBag.IdLoaiSanPham = new SelectList(db.LoaiSanPhams, "Id", "TenLoai", sanPham.IdLoaiSanPham);
            ViewBag.NguoiSua = new SelectList(db.QuanTris, "Id", "HoVaTen", sanPham.NguoiSua);
            ViewBag.NguoiThem = new SelectList(db.QuanTris, "Id", "HoVaTen", sanPham.NguoiThem);
            return View(sanPham);
        }

        // GET: admin/SanPhams/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SanPham sanPham = db.SanPhams.Find(id);
            if (sanPham == null)
            {
                return HttpNotFound();
            }
            return View(sanPham);
        }

        // POST: admin/SanPhams/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SanPham sanPham = db.SanPhams.Find(id);
            db.SanPhams.Remove(sanPham);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        protected string SaveAnh(HttpPostedFileBase Anh)
        {
            if (Anh != null && Anh.ContentLength > 0)
            {
                if (Anh.FileName.EndsWith(".jpg") || Anh.FileName.EndsWith(".jpeg") || Anh.FileName.EndsWith(".png"))
                {
                    Anh.SaveAs(Server.MapPath("~/Upload/images/" + Anh.FileName));
                    return Anh.FileName;
                }
                return "default.jpg";
            }
            return "default.jpg";
        }
    }
}
