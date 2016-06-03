using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eage.BPM.CoreCommon
{
    public class Keys
    {
        /// <summary>
        /// 用于存放验证码的session  key
        /// </summary>
        public const string vcode = "eageBpmVcode";
        /// <summary>
        /// 存放des加密key
        /// </summary>
        public static readonly string DESkey = "1QAX@wsx";
        /// <summary>
        /// 用于缓存某个用户的权限按钮数据的缓存key
        /// </summary>
        public const string PermissNavAndNavbuttonForUser = "PermissNavAndNavbuttonForUser";
        /// <summary>
        /// 用于缓存整个autofac的容器对象的缓存key
        /// </summary>
        public const string AutofacContainer = "eageBpmautofacContainer";
    }
}
