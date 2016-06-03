using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eage.BPM.CoreCommon
{
    public class PublicMethod
    {
        public static int GetInt(object obj)
        {
            if (obj != null)
            {
                int i;
                int.TryParse(obj.ToString(), out i);
                return i;
            }
            else
                return 0;
        }
        public static string GetString(object obj)
        {
            if (obj != null && obj != DBNull.Value)
                return obj.ToString();
            else
                return "";
        }
        /// <summary>
        /// 取得Int值,如果不成功则返回指定exceptionvalue值
        /// </summary>
        /// <param name="obj">要计算的值</param>
        /// <param name="exceptionvalue">异常时的返回值</param>
        /// <returns></returns>
        public static int GetInt(object obj, int exceptionvalue)
        {
            if (obj == null)
                return exceptionvalue;
            if (string.IsNullOrEmpty(obj.ToString()))
                return exceptionvalue;
            int i = exceptionvalue;
            try { i = Convert.ToInt32(obj); }
            catch { i = exceptionvalue; }
            return i;
        }
    }
}
