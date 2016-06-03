using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eage.BPM.WebHelper
{
    /// <summary>
    /// 自定义过滤器用于跳过登录，只能用于class和method    
    /// </summary>
    [AttributeUsage(AttributeTargets.Class|AttributeTargets.Method,AllowMultiple=false)]
    public class SkipCheckLogin:Attribute
    {
    }
}
