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
    /// <summary>
    /// 统一登录过滤器     
    /// </summary>
    public class CheckLoginAttribute : ActionFilterAttribute
    {
        //在方法执行前进行验证
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //在Controller上游skipchecklogin标签则跳过判断
            if (filterContext.ActionDescriptor.ControllerDescriptor.IsDefined(typeof(SkipCheckLogin), false))
            {
                return;
            }
            //在class之上打有skipchecklogin标签则跳过判断
            if (filterContext.ActionDescriptor.IsDefined(typeof(SkipCheckLogin), false))
            {
                return;
            }
            //判断当前session是否null,判断是否登录
            if (SysVisitor.Instance.UserID == (int)Enum.LoginStatus.NoLogin)
            {

                //判断cookie是否为null，如果成立则模拟用户的登录，再将用户实体数据存入session中
                if (filterContext.HttpContext.Request.Cookies[SysVisitor.CookieUserID] != null)
                {
                    //取出cookie中存入的uid的值
                    string desuid = filterContext.HttpContext.Request.Cookies[SysVisitor.CookieUserID].Value;
                    //将得到的id进行解密
                    string userid = StringHelper.DecryptDES(desuid, Keys.DESkey);
                    if (userid == null)
                    {
                        ToLogin(filterContext);
                    }
                    //从缓存中获取autofac的容器对象
                    var container = CacheHelper.Get<IContainer>(Keys.AutofacContainer);
                    //找autofac容器获取ISys_UsersService接口的具体实现类的对象实例
                    ISys_UsersService _userSer =container.Resolve<ISys_UsersService>();

                    //根据userSer 集合uid查询数据
                    int iuserid = int.Parse(userid);
                    var userinfo = _userSer.QueryWhere(c => c.KeyId == iuserid).FirstOrDefault();
                    if (userinfo != null)
                    {
                        //将当前用户信息存入session中
                        SysVisitor.Instance.UserID = userinfo.KeyId;
                        SysVisitor.Instance.UserName = userinfo.UserName;
                        SysVisitor.Instance.CurrentUser = userinfo;
                    }
                    else
                    {
                        ToLogin(filterContext);
                    }
                }
                else
                {

                    //跳转到登录页面
                    //filterContext.HttpContext.Response.Redirect("/admin/login/login");

                    //ContentResult cr = new ContentResult();
                    //cr.Content = "<script>alert('您未登录');window.location='/admin/login/login';</script>";

                    ToLogin(filterContext);
                }
            }
            base.OnActionExecuting(filterContext);
        }
        //没有登录
        private static void ToLogin(ActionExecutingContext filterContext)
        {
            //1.0 判断当前请求是否为一个ajax请求
            bool isAjaxRequst = filterContext.HttpContext.Request.IsAjaxRequest();

            if (isAjaxRequst)
            {
                //ajax的请求,则返回json格式
                JsonResult json = new JsonResult();
                json.Data = new { status = Enum.LoginStatus.NoLogin, msg = "您未登录或者登录已经失效，请重新登录" };
                json.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
                filterContext.Result = json;
            }
            else
            {
                //浏览器的请求
                ViewResult view = new ViewResult();
                view.ViewName = "/Areas/admin/Views/Shared/Tip.cshtml";
                filterContext.Result = view;
            }
        }
    }
}
