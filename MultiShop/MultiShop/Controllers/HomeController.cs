using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MutiShopDataContext.Model;
namespace MultiShop.Controllers
{
    public class HomeController : Controller
    {
        private DataContext db = new DataContext();
        public ActionResult Index()
        {
            return View();
        } 
        public PartialViewResult Footer()
        {
            
            return PartialView();
        }
        public PartialViewResult Header()
        {
            return PartialView(db.LoaiSanPhams.ToList());
        } 
        public PartialViewResult SlideTop()
        {
            return PartialView();
        }
        public PartialViewResult Banner()
        {
            return PartialView(db.DanhMucs.ToList());
        } 
        public PartialViewResult WomenBanner()
        {
            var listWomen = db.SanPhams.Where(s => s.IdDanhMuc == 3);
            return PartialView(listWomen.ToList());
        } 
        public PartialViewResult DealOfWeek()
        {
            return PartialView();
        }
        public PartialViewResult MenBanner()
        {
            var listWomen = db.SanPhams.Where(s => s.IdDanhMuc == 1);
            return PartialView(listWomen.ToList());
        }
        public PartialViewResult LastSpan()
        {
            return PartialView();
        }
         
    }
}