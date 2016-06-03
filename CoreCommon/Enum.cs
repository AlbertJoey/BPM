using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eage.BPM.CoreCommon
{
    public class Enum
    {
        public enum AjaxStatus
        {
            /// <summary>
            ///成功     
            /// </summary>
            Success = 0,
            /// <summary>
            ///提示  
            /// </summary>
            TipInfo = 1,
            /// <summary>
            ///警告     
            /// </summary>
            Warn = 2,
            /// <summary>
            ///错误     
            /// </summary>
            Error = 3,
            /// <summary>
            ///异常     
            /// </summary>
            Exception = 4

        }
        public enum LoginStatus
        {
            //登录成功
            Success = 0,
            //登录失败
            Error = 1,
            //登录系统异常
            Exception = 2,
            //未登录
            NoLogin = 3
        }
    }
}
