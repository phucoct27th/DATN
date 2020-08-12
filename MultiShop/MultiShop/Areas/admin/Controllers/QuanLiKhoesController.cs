using System;
using System.Collections.Generic;
using System.Data;
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
    [HasCredential(RoleID = "2")]
    public class QuanLiKhoesController : Controller
    {
        private DataContext db = new DataContext();

        // GET: admin/QuanLiKhoes
        public ActionResult Index(int? page)
        {
            int _pageSize = 1;
            int pageNumber = page ?? 1;
            var _listdanhmuc = db.Khoes.ToList();
            return View(_listdanhmuc.ToPagedList(pageNumber, _pageSize));
        }
        /*public ActionResult Index()
        {
            var quanLiKhoes = db.QuanLiKhoes.Include(q => q.Kho).Include(q => q.QuanTri).Include(q => q.QuanTri1).Include(q => q.SanPham);
            return View(quanLiKhoes.ToList());
        }*/

        // GET: admin/QuanLiKhoes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QuanLiKho quanLiKho = db.QuanLiKhoes.Find(id);
            if (quanLiKho == null)
            {
                return HttpNotFound();
            }
            return View(quanLiKho);
        }

        // GET: admin/QuanLiKhoes/Create
        public ActionResult Create()
        {
            ViewBag.IdKho = new SelectList(db.Khoes, "Id", "TenKho");
            ViewBag.NguoiSua = new SelectList(db.QuanTris, "Id", "HoVaTen");
            ViewBag.NguoiThem = new SelectList(db.QuanTris, "Id", "HoVaTen");
            ViewBag.IdSanPham = new SelectList(db.SanPhams, "Id", "TenSanPham");
            return View();
        }

        // POST: admin/QuanLiKhoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,IdKho,IdSanPham,NguoiNhap,NgayNhap,SoLuong,NguoiSua,NgaySua,NguoiThem,NgayThem")] QuanLiKho quanLiKho)
        {
            if (ModelState.IsValid)
            {
                var _listSpkho = db.QuanLiKhoes.Where(q => q.IdSanPham == quanLiKho.IdSanPham && q.IdKho == quanLiKho.IdKho).ToList();
                if(_listSpkho.Count== 0)
                {
                    quanLiKho.NguoiThem = 2;
                    quanLiKho.NgayNhap = DateTime.Now;
                    quanLiKho.SoLuong = 0;
                    db.QuanLiKhoes.Add(quanLiKho);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }    
               
            }

            ViewBag.IdKho = new SelectList(db.Khoes, "Id", "TenKho", quanLiKho.IdKho);
            ViewBag.NguoiSua = new SelectList(db.QuanTris, "Id", "HoVaTen", quanLiKho.NguoiSua);
            ViewBag.NguoiThem = new SelectList(db.QuanTris, "Id", "HoVaTen", quanLiKho.NguoiThem);
            ViewBag.IdSanPham = new SelectList(db.SanPhams, "Id", "TenSanPham", quanLiKho.IdSanPham);
            return View(quanLiKho);
        }

        // GET: admin/QuanLiKhoes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QuanLiKho quanLiKho = db.QuanLiKhoes.Find(id);
            if (quanLiKho == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdKho = new SelectList(db.Khoes, "Id", "TenKho", quanLiKho.IdKho);
            ViewBag.NguoiSua = new SelectList(db.QuanTris, "Id", "HoVaTen", quanLiKho.NguoiSua);
            ViewBag.NguoiThem = new SelectList(db.QuanTris, "Id", "HoVaTen", quanLiKho.NguoiThem);
            ViewBag.IdSanPham = new SelectList(db.SanPhams, "Id", "TenSanPham", quanLiKho.IdSanPham);
            return View(quanLiKho);
        }

        // POST: admin/QuanLiKhoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,IdKho,IdSanPham,NguoiNhap,NgayNhap,SoLuong,NguoiSua,NgaySua,NguoiThem,NgayThem")] QuanLiKho quanLiKho)
        {
            if (ModelState.IsValid)
            {
                db.Entry(quanLiKho).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdKho = new SelectList(db.Khoes, "Id", "TenKho", quanLiKho.IdKho);
            ViewBag.NguoiSua = new SelectList(db.QuanTris, "Id", "HoVaTen", quanLiKho.NguoiSua);
            ViewBag.NguoiThem = new SelectList(db.QuanTris, "Id", "HoVaTen", quanLiKho.NguoiThem);
            ViewBag.IdSanPham = new SelectList(db.SanPhams, "Id", "TenSanPham", quanLiKho.IdSanPham);
            return View(quanLiKho);
        }

        // GET: admin/QuanLiKhoes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QuanLiKho quanLiKho = db.QuanLiKhoes.Find(id);
            if (quanLiKho == null)
            {
                return HttpNotFound();
            }
            return View(quanLiKho);
        }

        // POST: admin/QuanLiKhoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
                QuanLiKho quanLiKho = db.QuanLiKhoes.Find(id);
                var nhapkho = db.NhapKhoes.Where(n => n.IdQuanLiKho == quanLiKho.Id);
                System.Diagnostics.Debug.WriteLine(nhapkho.ToList().Count);
                foreach (var item in nhapkho)
                {
                    db.NhapKhoes.Remove(item);
                    db.SaveChanges();
                }
                db.QuanLiKhoes.Remove(quanLiKho);
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
    }
}
