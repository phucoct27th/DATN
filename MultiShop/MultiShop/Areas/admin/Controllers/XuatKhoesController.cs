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
    public class XuatKhoesController : Controller
    {
        private DataContext db = new DataContext();

        // GET: admin/XuatKhoes
        public ActionResult Index(int? page)
        {
            int _pageSize = 20;
            int pageNumber = page ?? 1;
            var _listdanhmuc = db.XuatKhoes.Include(x => x.QuanLiKho).Include(x => x.QuanTri).ToList();
            return View(_listdanhmuc.ToPagedList(pageNumber, _pageSize));
        }
       /* public ActionResult Index()
        {
            var xuatKhoes = db.XuatKhoes.Include(x => x.QuanLiKho).Include(x => x.QuanTri);
            return View(xuatKhoes.ToList());
        }*/

        // GET: admin/XuatKhoes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            XuatKho xuatKho = db.XuatKhoes.Find(id);
            if (xuatKho == null)
            {
                return HttpNotFound();
            }
            return View(xuatKho);
        }

        // GET: admin/XuatKhoes/Create
        public ActionResult Create()
        {
            ViewBag.IdQuanLiKho = new SelectList(db.QuanLiKhoes, "Id", "Id");
            ViewBag.NguoiXuatKho = new SelectList(db.QuanTris, "Id", "HoVaTen");
            return View();
        }

        // POST: admin/XuatKhoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,IdQuanLiKho,NguoiXuatKho,NgayXuat,SoLuong")] XuatKho xuatKho)
        {
            if (ModelState.IsValid)
            {
                db.XuatKhoes.Add(xuatKho);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdQuanLiKho = new SelectList(db.QuanLiKhoes, "Id", "Id", xuatKho.IdQuanLiKho);
            ViewBag.NguoiXuatKho = new SelectList(db.QuanTris, "Id", "HoVaTen", xuatKho.NguoiXuatKho);
            return View(xuatKho);
        }

        // GET: admin/XuatKhoes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            XuatKho xuatKho = db.XuatKhoes.Find(id);
            if (xuatKho == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdQuanLiKho = new SelectList(db.QuanLiKhoes, "Id", "Id", xuatKho.IdQuanLiKho);
            ViewBag.NguoiXuatKho = new SelectList(db.QuanTris, "Id", "HoVaTen", xuatKho.NguoiXuatKho);
            return View(xuatKho);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,IdQuanLiKho,NguoiXuatKho,NgayXuat,SoLuong")] XuatKho xuatKho)
        {
            if (ModelState.IsValid)
            {
                db.Entry(xuatKho).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdQuanLiKho = new SelectList(db.QuanLiKhoes, "Id", "Id", xuatKho.IdQuanLiKho);
            ViewBag.NguoiXuatKho = new SelectList(db.QuanTris, "Id", "HoVaTen", xuatKho.NguoiXuatKho);
            return View(xuatKho);
        }

        // GET: admin/XuatKhoes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            XuatKho xuatKho = db.XuatKhoes.Find(id);
            if (xuatKho == null)
            {
                return HttpNotFound();
            }
            return View(xuatKho);
        }

        // POST: admin/XuatKhoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            XuatKho xuatKho = db.XuatKhoes.Find(id);
            db.XuatKhoes.Remove(xuatKho);
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
