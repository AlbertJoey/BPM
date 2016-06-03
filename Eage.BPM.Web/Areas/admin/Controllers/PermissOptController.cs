using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Eage.BPM.Web.Areas.admin.Controllers
{
    using Eage.BPM.CoreCommon;
    using Eage.BPM.IServices;
    using Eage.BPM.WebHelper;
    [SkipCheckPermiss, SkipCheckLogin]
    public class PermissOptController : BaseController
    {
        public PermissOptController(ISys_RoleNavBtnsService sysRoleNavBtnSer)
        {
            _sysRoleNavBtnSer = sysRoleNavBtnSer;
        }
        //获取当前nav下的所有功能按钮
        public ActionResult GetFunctions(string url)
        {
            int navid=GetNavID(url);
            try
            {
                var btnsList = _sysRoleNavBtnSer.GetPermissForUserByCache(SysVisitor.Instance.UserID).Where(c => c.NavId == navid);
                string splitTag = @"<div class='datagrid-btn-separator'></div>";
                if (btnsList.Any())
                {
                    string[] btnHtmlArr = (from btn in btnsList
                                           where btn.ButtonTag.ToLower() != "browser"   //隐藏浏览按钮
                                           orderby btn.btnSortnum
                                           select btn.ButtonHtml).ToArray<string>();
                    splitTag = string.Join(splitTag, btnHtmlArr);
                }
                //返回
                return Content(splitTag);
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        private int GetNavID(string url)
        {
            if (url != "")
            {
                return Convert.ToInt32(url.Split('?')[0].Split('/')[4]);
            }
            else
                return -1;
        }
    }
}