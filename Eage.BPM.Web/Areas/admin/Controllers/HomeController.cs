using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Eage.BPM.Web.Areas.admin.Controllers
{
    using Eage.BPM.WebHelper;
    using Eage.BPM.IServices;
    using Eage.BPM.CoreCommon;
    public class HomeController : BaseController
    {
        public HomeController(ISys_RoleNavBtnsService sysRolenavBtnSer)
        {
            _sysRoleNavBtnSer = sysRolenavBtnSer;
        }
        //
        // GET: /admin/Home/
        [SkipCheckPermiss]
        public ActionResult Index()
        {
            return View();
        }
        [SkipCheckPermiss]
        public ActionResult GetMenuData()
        {
            //从缓存中读取navList
            var navList =
                (from r in _sysRoleNavBtnSer.GetPermissForUserByCache(SysVisitor.Instance.UserID)
                 select new
                 {
                     NavId = r.NavId,
                     NavTitle = r.NavTitle,
                     Linkurl = r.Linkurl,
                     Sortnum = r.navSortnum,
                     navIconCls = r.navIconCls,
                     navIconUrl = r.navIconUrl,
                     ParentID = r.ParentID,
                     NavArea = r.NavArea,
                     NavAction = r.NavAction,
                     NavController = r.NavController
                 }).Distinct().ToList();
            //对navList进行迭代按照指定格式数据返回
            var menuData = " var menus = [ ";
            var parnavcount=0;
            var chinavcount =0;
            foreach (var parNav in navList)
            {
                if (parNav.ParentID == 0)
                {
                    menuData += "{\"id\":\"" + parNav.NavId + "\",\"text\":\"" + parNav.NavTitle + "\",\"iconCls\":\"" + parNav.navIconCls + "\",\"attributes\":";
                    menuData += "{\"url\":\"" + parNav.NavArea + "/" + parNav.NavController + "/" + parNav.NavAction + "\",\"iconUrl\":\"" + parNav.navIconUrl + "\",\"parentid\":\"" + parNav.ParentID + "\",\"sortnum\":\"" + parNav.Sortnum + "\"}";
                    menuData += ",\"children\":[";
                    foreach (var chiNav in navList)
                    {
                        if (chiNav.ParentID == parNav.NavId)
                        {
                            menuData += "{\"id\":\"" + chiNav.NavId + "\",\"text\":\"" + chiNav.NavTitle + "\",\"iconCls\":\"" + chiNav.navIconCls + "\",\"attributes\":";
                            menuData += "{\"url\":\"/" + chiNav.NavArea + "/" + chiNav.NavController + "/" + chiNav.NavAction + "\",\"iconUrl\":\"" + chiNav.navIconUrl + "\",\"parentid\":\"" + chiNav.ParentID + "\",\"sortnum\":\"" + chiNav.Sortnum + "\"}";
                            menuData += ",\"children\":[]},";
                            chinavcount++;
                        }
                    }
                    if (chinavcount != 0)
                        menuData = menuData.Remove(menuData.Length - 1);
                    menuData += "]},";
                    parnavcount++;
                }
            }
            if (parnavcount != 0)
                menuData = menuData.Remove(menuData.Length - 1);
            menuData += "]";
            return Content(menuData);
        }
        [SkipCheckPermiss]
        public ActionResult Welcome()
        {
            return View();
        }
    }
}
