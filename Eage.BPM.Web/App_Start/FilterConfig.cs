using System.Web;
using System.Web.Mvc;

namespace Eage.BPM.Web
{
    using WebHelper.Filter;
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());

            //将登录过滤器注册为全局过滤器
            filters.Add(new CheckLoginAttribute());

            //将权限过滤器注册为全局过滤器
            filters.Add(new CheckPermissAttribute());
        }
    }
}