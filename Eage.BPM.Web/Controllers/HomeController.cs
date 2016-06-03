using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Eage.BPM.Web.Controllers
{
    using Eage.BPM.IServices;
    using Eage.BPM.ViewModel;
    using Eage.BPM.Model;
    using Eage.BPM.EntityMapping;
    using Eage.BPM.WebHelper;

     [SkipCheckLogin, SkipCheckPermiss]
    public class HomeController:BaseController
    {
        // GET: /Home/
        public ActionResult Index()
        {
            Response.Redirect("Admin/Home/Index");
            return View();
        }

    }
}
