
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using MultiShop.Common;
using MutiShopDataContext.Model;
using PagedList;

namespace MultiShop.Areas.admin.Controllers
{
    [HasCredential(RoleID = "4")]
    public class DatHangsController : Controller
    {
        private DataContext db = new DataContext();

        // GET: admin/DatHangs
        public ActionResult Index(int? page)
        {
            int _pageSize = 20;
            int pageNumber = page ?? 1;
            var _listdanhmuc = db.DatHangs.Include(d => d.KhachHang).Include(d => d.SanPham).ToList();
            return View(_listdanhmuc.ToPagedList(pageNumber, _pageSize));
        }
       /* public ActionResult Index()
        {
            var datHangs = db.DatHangs.Include(d => d.KhachHang).Include(d => d.SanPham);
            return View(datHangs.ToList());
        }
*/
        // GET: admin/DatHangs/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DatHang datHang = db.DatHangs.Find(id);
            if (datHang == null)
            {
                return HttpNotFound();
            }
            return View(datHang);
        }

        // GET: admin/DatHangs/Create
        public ActionResult Create()
        {
            ViewBag.NguoiDat = new SelectList(db.KhachHangs, "Id", "HoVaTen");
            ViewBag.IdSanPham = new SelectList(db.SanPhams, "Id", "TenSanPham");
            return View();
        }

        // POST: admin/DatHangs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaDat,NguoiDat,IdSanPham,TrangThai,SoLuong,DiaChiGiaoHang")] DatHang datHang)
        {
            if (ModelState.IsValid)
            {
                db.DatHangs.Add(datHang);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.NguoiDat = new SelectList(db.KhachHangs, "Id", "HoVaTen", datHang.NguoiDat);
            ViewBag.IdSanPham = new SelectList(db.SanPhams, "Id", "TenSanPham", datHang.IdSanPham);
            return View(datHang);
        }

        // GET: admin/DatHangs/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DatHang datHang = db.DatHangs.Find(id);
            if (datHang == null)
            {
                return HttpNotFound();
            }
            ViewBag.NguoiDat = new SelectList(db.KhachHangs, "Id", "HoVaTen", datHang.NguoiDat);
            ViewBag.IdSanPham = new SelectList(db.SanPhams, "Id", "TenSanPham", datHang.IdSanPham);
            return View(datHang);
        }

        // POST: admin/DatHangs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaDat,NguoiDat,IdSanPham,TrangThai,SoLuong,DiaChiGiaoHang")] DatHang datHang)
        {
            if (ModelState.IsValid)
            {
                db.Entry(datHang).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.NguoiDat = new SelectList(db.KhachHangs, "Id", "HoVaTen", datHang.NguoiDat);
            ViewBag.IdSanPham = new SelectList(db.SanPhams, "Id", "TenSanPham", datHang.IdSanPham);
            return View(datHang);
        }

        // GET: admin/DatHangs/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DatHang datHang = db.DatHangs.Find(id);
            if (datHang == null)
            {
                return HttpNotFound();
            }
            return View(datHang);
        }

        // POST: admin/DatHangs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            DatHang datHang = db.DatHangs.Find(id);
            db.DatHangs.Remove(datHang);
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
