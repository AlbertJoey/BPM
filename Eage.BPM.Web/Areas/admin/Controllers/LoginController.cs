using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Eage.BPM.Web.Areas.admin.Controllers
{
    using Eage.BPM.ViewModel;
    using Eage.BPM.IServices;
    using Eage.BPM.WebHelper;
    using Eage.BPM.CoreCommon;
    using RulesEngine;
    [SkipCheckLogin, SkipCheckPermiss]
    public class LoginController : BaseController
    {
        public LoginController(ISys_UsersService sysUserSer,ISys_RoleNavBtnsService sysRoleNavBtnSer)
        {
            _sysUserSer = sysUserSer;
            _sysRoleNavBtnSer = sysRoleNavBtnSer;
        }
        //
        // GET: /admin/Login/
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(LoginModelView loginmodel)
        {
            try
            {
                var sessionVcode = SysVisitor.Instance.Vcode;
                engine.For<LoginModelView>()
                        .Setup(p => p.UserName)
                           .MustNotBeNullOrEmpty()
                           .WithMessage("登录名不能为空！！")
                        .Setup(p => (p.Password ?? "").Length)
                           .MustBeBetween(6, 16)
                           .WithMessage("密码必须在{0}-{1}之间！！")
                        .Setup(p => p.Vcode.ToLower())
                           .MustNotBeNullOrEmpty()
                           .WithMessage("验证码不能为空！！")
                           .MustEqual(sessionVcode.ToLower())
                           .WithMessage("验证码输入有误！！")
                        .Setup(p => p.CookieDay)
                           .MustBeOfType(typeof(int))
                           .WithMessage("cookie必须是整数形式！！");
                var str = myValid.ErrorList(engine, loginmodel);
                //校验失败
                if (!myValid.IsValid(engine, loginmodel))
                {
                    return WriteErrorInfo(myValid.ErrorFirst(engine, loginmodel));
                }
                else
                {
                    //校验登录是否成功
                    //根据当前传过来的用户名获取用户
                    var user = _sysUserSer.QueryWhere(c => c.UserName == loginmodel.UserName).FirstOrDefault();
                    if (user == null)
                    {
                        return WriteErrorInfo("用户名不存在！！");
                    }
                    else
                    {
                        var newPassword = StringHelper.MD5string(loginmodel.Password + user.PassSalt);
                        if (newPassword != user.Password)
                        {
                            return WriteErrorInfo("密码错误！！");
                        }
                        //将当前登录成功的用户存入session中
                        SysVisitor.Instance.UserID = user.KeyId;
                        SysVisitor.Instance.UserName = user.UserName;
                        SysVisitor.Instance.CurrentUser = user;
                        if (loginmodel.CookieDay != -1)
                        {
                            //将当前登录的用户ID进行DES加密
                            var desUserID = StringHelper.EncryptDES(user.KeyId.ToString(), Keys.DESkey);
                            //将当前加密后的userID存入cookie中
                            HttpCookie cookie = new HttpCookie(SysVisitor.CookieUserID, desUserID);
                            cookie.Expires = DateTime.Now.AddDays(loginmodel.CookieDay);
                            Response.Cookies.Add(cookie);
                        }
                        //将当前登录用户的左侧菜单权限和按钮权限存入缓存中,当给角色分配权限的时候缓存失效
                        _sysRoleNavBtnSer.GetPermissForUserByCache(user.KeyId);

                        return WriteSuccessInfo("登录成功！！");
                    }
                }
            }
            catch (Exception ex)
            {

                return WriteErrorInfo(ex.Message);
            }
        }
        public ActionResult LoginForm()
        {
            return View();
        }
        public ActionResult showValidateCode()
        {
            var showValidateCode = ConfigHelper.GetValue("showValidateCode");
            showValidateCode = " var  showValidateCode = " + showValidateCode;
            return Content(showValidateCode);
        }
    }
}
