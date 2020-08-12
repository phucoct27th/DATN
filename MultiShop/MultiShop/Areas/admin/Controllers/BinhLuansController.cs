using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MutiShopDataContext.Model;
using PagedList;

namespace MultiShop.Areas.admin.Controllers
{
    public class BinhLuansController : Controller
    {
        private DataContext db = new DataContext();

        // GET: admin/BinhLuans

       /* public ActionResult Index()
        {
            var binhLuans = db.BinhLuans.Include(b => b.KhachHang).Include(b => b.SanPham);
            return View(binhLuans.ToList());
        }*/
        public ActionResult Index(int? page)
        {
            int _pageSize = 20;
            int pageNumber = page ?? 1;
            var _listBl = db.BinhLuans.Include(d => d.KhachHang).Include(d => d.SanPham).ToList();
            return View(_listBl.ToPagedList(pageNumber, _pageSize));
        }
        // GET: admin/BinhLuans/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BinhLuan binhLuan = db.BinhLuans.Find(id);
            if (binhLuan == null)
            {
                return HttpNotFound();
            }
            return View(binhLuan);
        }

        // GET: admin/BinhLuans/Create
        public ActionResult Create()
        {
            ViewBag.IdKhachHang = new SelectList(db.KhachHangs, "Id", "HoVaTen");
            ViewBag.IdSanPham = new SelectList(db.SanPhams, "Id", "TenSanPham");
            return View();
        }

        // POST: admin/BinhLuans/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,IdSanPham,NoiDung,IdKhachHang")] BinhLuan binhLuan)
        {
            if (ModelState.IsValid)
            {
                db.BinhLuans.Add(binhLuan);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdKhachHang = new SelectList(db.KhachHangs, "Id", "HoVaTen", binhLuan.IdKhachHang);
            ViewBag.IdSanPham = new SelectList(db.SanPhams, "Id", "TenSanPham", binhLuan.IdSanPham);
            return View(binhLuan);
        }

        // GET: admin/BinhLuans/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BinhLuan binhLuan = db.BinhLuans.Find(id);
            if (binhLuan == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdKhachHang = new SelectList(db.KhachHangs, "Id", "HoVaTen", binhLuan.IdKhachHang);
            ViewBag.IdSanPham = new SelectList(db.SanPhams, "Id", "TenSanPham", binhLuan.IdSanPham);
            return View(binhLuan);
        }

        // POST: admin/BinhLuans/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,IdSanPham,NoiDung,IdKhachHang")] BinhLuan binhLuan)
        {
            if (ModelState.IsValid)
            {
                db.Entry(binhLuan).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdKhachHang = new SelectList(db.KhachHangs, "Id", "HoVaTen", binhLuan.IdKhachHang);
            ViewBag.IdSanPham = new SelectList(db.SanPhams, "Id", "TenSanPham", binhLuan.IdSanPham);
            return View(binhLuan);
        }

        // GET: admin/BinhLuans/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BinhLuan binhLuan = db.BinhLuans.Find(id);
            if (binhLuan == null)
            {
                return HttpNotFound();
            }
            return View(binhLuan);
        }

        // POST: admin/BinhLuans/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BinhLuan binhLuan = db.BinhLuans.Find(id);
            db.BinhLuans.Remove(binhLuan);
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
