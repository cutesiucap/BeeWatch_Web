using MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC.Controllers
{
    public class AuthorizeCustom : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            Accounts accounts = HttpContext.Current.Session["Account"] as Accounts;
            //nếu session=null thì trả về trang đăng nhập
            if (accounts!=null && accounts.Username != null)
            {
                //lấy loại tài khoản
                var account_type = accounts.Account_Type;
                //lấy các loại quyền
                var authorize = account_type.Authoriza;
                //đếm số lượng quyền
                int authorize_count = authorize.Count();
                //khởi tạo mảng
                string[] listaction = new string[authorize_count];
                int i = 0;
                //lấy danh sách quyền đưa vào mảng
                foreach (var item in authorize)
                {
                    listaction[i++] = item.Action.Name;
                }
                //Lấy tên controller và action
                string actionName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName + "/" + filterContext.ActionDescriptor.ActionName;

                HttpContext.Current.Session["BackAction"] = actionName;
                //Lấy tên Controller
                //string ControllerName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
                //nếu tên controler không có trong mảng quyền của user thì trả về trang đăng nhập
                if (!listaction.Contains(actionName))
                {
                    switch (account_type.Name)
                    {
                        case "Customer":
                            filterContext.Result = new RedirectResult("~/Sellers/LoginSeller");
                            break;
                        case "Seller":
                            filterContext.Result = new RedirectResult("~/Admin/Login");
                            break;
                    }
                }
            }
            //session != null
            else
            {
                filterContext.Result = new RedirectResult("~/Sellers/LoginSeller");
                
            }
        }
    }
}