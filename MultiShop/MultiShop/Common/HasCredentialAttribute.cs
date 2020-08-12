using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Script.Serialization;
using MutiShopDataContext.Heper;
namespace MultiShop.Common
{
     public class HasCredentialAttribute : AuthorizeAttribute
    {
        private bool _isAuthorized;
        private  bool iRoleCheck;
        public string RoleID { set; get; }
        protected override bool AuthorizeCore(HttpContextBase httpContext)
         {
             var getcookie = HttpContext.Current.Request.Cookies["autoiadmin"];
             if(getcookie ==null)
             {
                 _isAuthorized = true;
                 iRoleCheck = false;
                 return false;
             }
             if(this.RoleID=="DANGNHAP")
             {
                 return true;
             }
             JavaScriptSerializer jss = new JavaScriptSerializer();
             CookieAdmin cookieAdmin = jss.Deserialize<CookieAdmin>(getcookie.Value);
             List<string> rolesProvider = new List<string>();
             for (int i = 0; i < cookieAdmin.Phanquyens.Count; i++)
             {
                 rolesProvider.Add(cookieAdmin.Phanquyens[i].IdQuyen);
             }
             if(rolesProvider.Contains("1"))
             {
                return true;
             }    
             if (rolesProvider.Contains(this.RoleID))
             {
                iRoleCheck = false;
                return true;
             }
             else
             {
                iRoleCheck = true;
                return false;
             }            
         }
         protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
         {
             if (_isAuthorized ==true)
             {
                 filterContext.Result = new RedirectToRouteResult(new
                     RouteValueDictionary(new { controller = "Autention", action = "Login", area = "admin" }));
             }
             if (iRoleCheck == true)
             {
                 filterContext.Result = new ViewResult
                 {
                     ViewName = "~/Views/Shared/Erorr401.cshtml"
                 };
             }

         }
    }
}