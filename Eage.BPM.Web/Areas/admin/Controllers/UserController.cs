using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Eage.BPM.Web.Areas.admin.Controllers
{
    using Eage.BPM.IServices;
    using Eage.BPM.WebHelper;
    public class UserController : BaseController
    {
        public UserController(ISys_UsersService sysUserSer)
        {
            _sysUserSer = sysUserSer;
        }
        //
        // GET: admin/User
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost,SkipCheckPermiss]
        public ActionResult GetList()
        {
            int outCount =0;
            var list = _sysUserSer.QueryByPage(c => c.UserName == "admin2", c => c.KeyId, 50, 1, out outCount);
            return Json(new { rows = list, total = outCount });
        } 
    }
}