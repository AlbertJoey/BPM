using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Eage.BPM.WebHelper
{
    using System.Web.Mvc;
    using IServices;
    using CoreCommon;
    using RulesEngine;
    using Eage.BPM.Cache;
    using Autofac;
    using Eage.BPM.Model;
    using System.Web;
    public class BaseController : Controller
    {
        //定义所有业务逻辑类的接口
        protected  ISys_ButtonsService _sysBtnSer;
        protected  ISys_DepartmentsService _sysDepSer;
        protected  ISys_LogDetailsService _sysLogSer;
        protected  ISys_NavButtonsService _sysNavBtnSer;
        protected  ISys_NavigationsService _sysNavGationSer;
        protected  ISys_RoleNavBtnsService _sysRoleNavBtnSer;
        protected  ISys_RolesService _sysRoleSer;
        protected  ISys_Roles_DepartmentsService _sysRoleDepSer;
        protected  ISys_UserNavBtnsService _sysUserNavBtnSer;
        protected  ISys_UserRolesService _sysUserRoleSer;
        protected  ISys_UsersService _sysUserSer;
        protected  ISys_Users_DepartmentsService _sysUserDepSer;
        protected  ILogService _logSer;
        /// <summary>
        /// 定义一个实例关于RulesEngine(校验)
        /// </summary>
        protected MyRulesEngine myValid = new MyRulesEngine();
        protected Engine engine = new Engine();

        #region 成功信息
        public ActionResult WriteSuccessInfo(string successInfo)
        {
            return this.WriteSuccessInfo(successInfo, null);
        }
        public ActionResult WriteSuccessInfo(string successInfo, object data)
        {
            return Json(new JsonMessage { Status = (int)Enum.AjaxStatus.Success, Msg = successInfo, Success = true, Data = data });

        }
        #endregion

        #region 提示信息json
        /// <summary>
        /// 提示信息
        /// </summary>
        /// <param name="TipInfo" type="string">
        ///     <para>
        ///         提示的信息
        ///     </para>
        /// </param>
        /// <returns>
        ///     A string value...
        /// </returns>
        public ActionResult WriteTipInfo(string tipInfo)
        {
            return this.WriteTipInfo(tipInfo, null);
        }
        public ActionResult WriteTipInfo(string tipInfo, object data)
        {
            return Json(new JsonMessage { Status = (int)Enum.AjaxStatus.TipInfo, Msg = tipInfo, Success = true, Data = data });
        }
        #endregion

        #region 警告信息json
        public ActionResult WriteWarnInfo(string warnInfo)
        {
            return this.WriteWarnInfo(warnInfo);
        }
        public ActionResult WriteWarnInfo(string warnInfo, object data)
        {
            return Json(new JsonMessage { Status = (int)Enum.AjaxStatus.Warn, Msg = warnInfo, Success = true, Data = data });
        }
        #endregion

        #region 错误信息json
        public ActionResult WriteErrorInfo(string errorInfo)
        {
            return this.WriteErrorInfo(errorInfo, null);
        }
        public ActionResult WriteErrorInfo(string errorInfo, object data)
        {
            return Json(new JsonMessage { Status = (int)Enum.AjaxStatus.Error, Msg = errorInfo, Success = false, Data = data });
        }
        #endregion

        #region 系统异常json
        public ActionResult WriteExpInfo(string expInfo)
        {
            return this.WriteExpInfo(expInfo);
        }
        public ActionResult WriteExpInfo(string expInfo, object data)
        {
            return Json(new JsonMessage { Status = (int)Enum.AjaxStatus.Exception, Msg = expInfo, Success = false, Data = data });
        }
        #endregion

        [SkipCheckPermiss]
        public string Toolbar(HttpRequestBase req)
        {
            ////控制名称
            //string controllerName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName.ToLower();
            ////获取区域的名称
            //string areaName = string.Empty;
            //if (filterContext.RouteData.DataTokens.ContainsKey("area"))
            //{
            //    areaName = filterContext.RouteData.DataTokens["area"].ToString().ToLower();
            //}
            //从缓存中获取autofac的容器对象
            var container = CacheHelper.Get<IContainer>(Keys.AutofacContainer);
            //注入指定类型接口
            ISys_RoleNavBtnsService _iperSer = container.Resolve<ISys_RoleNavBtnsService>();
            var reqPath = req.FilePath.Remove(1).ToLower();
            //从缓存中读取当前用户在此页面上拥有的功能按钮
            var list = _iperSer.GetPermissForUserByCache(SysVisitor.Instance.UserID)
                .Where(c => c.Linkurl.ToLower() == req.FilePath.Remove(1).ToLower()).ToList <Usp_GetNavAndNavbuttonPermissByUserID_Result>();
            string btns="";
            if (list != null)
                btns = pageButtons(list);
            return btns;
        }
        private string pageButtons(List<Usp_GetNavAndNavbuttonPermissByUserID_Result> btns)
        {
            const string splitTag = @"<div class='datagrid-btn-separator'></div>";
            if (btns!=null)
            {
                string[] btnHtmlArr = (from btn in btns
                                       where btn.ButtonTag.ToLower() != "browser"   //隐藏浏览按钮
                                       orderby btn.btnSortnum
                                       select btn.ButtonHtml).ToArray<string>();
                return string.Join(splitTag, btnHtmlArr);
            }
            return "";
        }

    }
    public class JsonMessage
    {
        public bool Success { get; set; }
        public int Status { get; set; }
        public string Msg { get; set; }
        public object Data { get; set; }
    }
}
