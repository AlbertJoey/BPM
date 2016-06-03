using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Eage.BPM.WebHelper.Filter
{
    using CoreCommon;
    using System.Web.Mvc;
    using Eage.BPM.Cache;
    using IServices;
    using Autofac.Integration.Mvc;
    using Autofac;
    public class CheckPermissAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //在Controller上游SkipCheckPermiss标签则跳过判断
            if (filterContext.ActionDescriptor.ControllerDescriptor.IsDefined(typeof(SkipCheckPermiss), false))
            {
                return;
            }
            //在class之上打有SkipCheckPermiss标签则跳过判断
            if (filterContext.ActionDescriptor.IsDefined(typeof(SkipCheckPermiss), false))
            {
                return;
            }
            //获取当前触发此OnActionExecuting的aciton
            string actionName = filterContext.ActionDescriptor.ActionName.ToLower();

            //控制名称
            string controllerName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName.ToLower();

            //获取区域的名称
            string areaName = string.Empty;
            if (filterContext.RouteData.DataTokens.ContainsKey("area"))
            {
                areaName = filterContext.RouteData.DataTokens["area"].ToString().ToLower();
            }
            //从缓存中获取autofac的容器对象
            var container = CacheHelper.Get<IContainer>(Keys.AutofacContainer);
            //注入指定类型接口
            ISys_RoleNavBtnsService _iperSer = container.Resolve<ISys_RoleNavBtnsService>();
            //从缓存中读取当前用户所拥有的权限
            var list = _iperSer.GetPermissForUserByCache(SysVisitor.Instance.UserID);

            //根据上面的三个成员的值作为条件去当前用户的权限按钮缓存数据查找，如果没有找到则表示没有权限
            var isPermiss = false;
            if (list != null)
            {
                isPermiss = list.Any(c => c.NavArea.ToLower() == areaName
                      && c.NavController.ToLower() == controllerName
                      && c.NavAction.ToLower() == actionName);
            }
            if (isPermiss == false)
            {
                NoPermiss(filterContext);
            }

        }
        //没有权限方法
        private static void NoPermiss(ActionExecutingContext filterContext)
        {
            //1.0 判断当前请求是否为一个ajax请求
            bool isAjaxRequst = filterContext.HttpContext.Request.IsAjaxRequest();

            if (isAjaxRequst)
            {
                //ajax的请求,则返回json格式
                JsonResult json = new JsonResult();
                json.Data = new { status = Enum.AjaxStatus.Error, msg = "您没有权限访问此页面" };
                json.JsonRequestBehavior = JsonRequestBehavior.AllowGet;

                filterContext.Result = json;
            }
            else
            {
                //浏览器的请求
                ViewResult view = new ViewResult();
                view.ViewName = "/Areas/admin/Views/Shared/NoPermiss.cshtml";
                filterContext.Result = view;
            }
        }
    }
}
