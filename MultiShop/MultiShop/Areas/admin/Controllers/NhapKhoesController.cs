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
    [HasCredential(RoleID = "2")]
    public class NhapKhoesController : Controller
    {
        private DataContext db = new DataContext();

        // GET: admin/NhapKhoes
        public ActionResult Index(int? page)
        {
            int _pageSize = 20;
            int pageNumber = page ?? 1;
            var _listdanhmuc = db.NhapKhoes.Include(n => n.QuanLiKho).Include(n => n.QuanTri).ToList();
            return View(_listdanhmuc.ToPagedList(pageNumber, _pageSize));
        }
        /*public ActionResult Index()
        {
            var nhapKhoes = db.NhapKhoes.Include(n => n.QuanLiKho).Include(n => n.QuanTri);
            return View(nhapKhoes.ToList());
        }*/

        // GET: admin/NhapKhoes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NhapKho nhapKho = db.NhapKhoes.Find(id);
            if (nhapKho == null)
            {
                return HttpNotFound();
            }
            return View(nhapKho);
        }

        // GET: admin/NhapKhoes/Create
        public ActionResult Create()
        {
            ViewBag.qlkho = db.QuanLiKhoes.ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,NguoiNhap,NgayNhap,IdQuanLiKho,SoLuong")] NhapKho nhapKho)
        {
            if (ModelState.IsValid)
            {
                nhapKho.NgayNhap = DateTime.Now;
                nhapKho.NguoiNhap = 2;
                db.NhapKhoes.Add(nhapKho);
                var quanlikho = db.QuanLiKhoes.Find(nhapKho.IdQuanLiKho);
                quanlikho.SoLuong += nhapKho.SoLuong;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.qlkho = db.QuanLiKhoes.ToList();
            return View(nhapKho);
        }

        // GET: admin/NhapKhoes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NhapKho nhapKho = db.NhapKhoes.Find(id);
            if (nhapKho == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdQuanLiKho = new SelectList(db.QuanLiKhoes, "Id", "Id", nhapKho.IdQuanLiKho);
            ViewBag.NguoiNhap = new SelectList(db.QuanTris, "Id", "HoVaTen", nhapKho.NguoiNhap);
            return View(nhapKho);
        }

        // POST: admin/NhapKhoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,NguoiNhap,NgayNhap,IdQuanLiKho,SoLuong")] NhapKho nhapKho)
        {
            if (ModelState.IsValid)
            {
                db.Entry(nhapKho).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdQuanLiKho = new SelectList(db.QuanLiKhoes, "Id", "Id", nhapKho.IdQuanLiKho);
            ViewBag.NguoiNhap = new SelectList(db.QuanTris, "Id", "HoVaTen", nhapKho.NguoiNhap);
            return View(nhapKho);
        }

        // GET: admin/NhapKhoes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NhapKho nhapKho = db.NhapKhoes.Find(id);
            if (nhapKho == null)
            {
                return HttpNotFound();
            }
            return View(nhapKho);
        }

        // POST: admin/NhapKhoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            NhapKho nhapKho = db.NhapKhoes.Find(id);
            db.NhapKhoes.Remove(nhapKho);
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
