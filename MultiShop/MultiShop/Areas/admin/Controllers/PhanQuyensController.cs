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
    [HasCredential(RoleID = "1")]
    public class PhanQuyensController : Controller
    {
        private DataContext db = new DataContext();
        // GET: admin/PhanQuyens
        public ActionResult Index(int? page)
        {
            int _pageSize = 20;
            int pageNumber = page ?? 1;
            var phanQuyens = db.PhanQuyens.Include(p => p.QuanTri).Include(p => p.Quyen).ToList();
            return View(phanQuyens.ToPagedList(pageNumber, _pageSize));
        }

        // GET: admin/PhanQuyens/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PhanQuyen phanQuyen = db.PhanQuyens.Find(id);
            if (phanQuyen == null)
            {
                return HttpNotFound();
            }
            return View(phanQuyen);
        }

        // GET: admin/PhanQuyens/Create
        public ActionResult Create()
        {
            ViewBag.IdQuanTri = new SelectList(db.QuanTris, "Id", "HoVaTen");
            ViewBag.IdQuyen = new SelectList(db.Quyens, "Id", "MoTa");
            return View();
        }

        // POST: admin/PhanQuyens/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,IdQuyen,IdQuanTri")] PhanQuyen phanQuyen)
        {
            if (ModelState.IsValid)
            {
                var _listpq = db.PhanQuyens.Where(p => p.IdQuanTri == phanQuyen.IdQuanTri).ToList();
                if(_listpq.Count==0)
                {
                    db.PhanQuyens.Add(phanQuyen);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }    
                
            }

            ViewBag.IdQuanTri = new SelectList(db.QuanTris, "Id", "HoVaTen", phanQuyen.IdQuanTri);
            ViewBag.IdQuyen = new SelectList(db.Quyens, "Id", "MoTa", phanQuyen.IdQuyen);
            return View(phanQuyen);
        }

        // GET: admin/PhanQuyens/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PhanQuyen phanQuyen = db.PhanQuyens.Find(id);
            if (phanQuyen == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdQuanTri = new SelectList(db.QuanTris, "Id", "HoVaTen", phanQuyen.IdQuanTri);
            ViewBag.IdQuyen = new SelectList(db.Quyens, "Id", "MoTa", phanQuyen.IdQuyen);
            return View(phanQuyen);
        }

        // POST: admin/PhanQuyens/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,IdQuyen,IdQuanTri")] PhanQuyen phanQuyen)
        {
            if (ModelState.IsValid)
            {
                db.Entry(phanQuyen).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdQuanTri = new SelectList(db.QuanTris, "Id", "HoVaTen", phanQuyen.IdQuanTri);
            ViewBag.IdQuyen = new SelectList(db.Quyens, "Id", "MoTa", phanQuyen.IdQuyen);
            return View(phanQuyen);
        }

        // GET: admin/PhanQuyens/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PhanQuyen phanQuyen = db.PhanQuyens.Find(id);
            if (phanQuyen == null)
            {
                return HttpNotFound();
            }
            return View(phanQuyen);
        }

        // POST: admin/PhanQuyens/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PhanQuyen phanQuyen = db.PhanQuyens.Find(id);
            db.PhanQuyens.Remove(phanQuyen);
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
