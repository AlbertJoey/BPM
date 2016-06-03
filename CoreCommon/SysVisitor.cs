using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eage.BPM.CoreCommon
{
    using Model;
    using System.Web;
    using System.Web.Mvc;
    public class SysVisitor
    {
        public static SysVisitor Instance
        {
            get { return SingletonProvider<SysVisitor>.Instance; }
        }

        public const string SessionUserID = "eageBpmSessionUserID";
        public const string SessionUserName = "eageBpmSessionUserName";
        public const string SessionUser = "eageBpmSessionUser";

        public const string EageBpmVcode = "eageBpmVcode";

        public const string CookieUserID = "eageBpmUserID";
        public int UserID
        {
            get { return PublicMethod.GetInt(HttpContext.Current.Session[SessionUserID], (int)Enum.LoginStatus.NoLogin); }
            set { HttpContext.Current.Session[SessionUserID] = value; }
        }
        public string UserName
        {
            get { return PublicMethod.GetString(HttpContext.Current.Session[SessionUserName]); }
            set { HttpContext.Current.Session[SessionUserName] = value; }
        }
        public Sys_Users CurrentUser
        {
            get { return (Sys_Users)HttpContext.Current.Session[SessionUser]; }
            set { HttpContext.Current.Session[SessionUser] = value; }
        }
        public string Vcode
        {
            get { return PublicMethod.GetString(HttpContext.Current.Session[EageBpmVcode]); }
            set { HttpContext.Current.Session[EageBpmVcode] = value; }
        }
    }
}
