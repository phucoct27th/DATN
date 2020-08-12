using MultiShop.Common;
using System.Web.Mvc;
namespace MultiShop.Areas.admin.Controllers
{
    [HasCredential(RoleID = "DANGNHAP")]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        public PartialViewResult MainHeader()
        {
            return PartialView();
        }
        public PartialViewResult Footer()
        {
            return PartialView();
        } 
        public PartialViewResult MainHome()
        {
            return PartialView();
        } 
        public PartialViewResult Menu()
        {
            return PartialView();
        }
    }
}