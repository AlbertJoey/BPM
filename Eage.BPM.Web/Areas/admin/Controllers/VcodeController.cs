using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Eage.BPM.Web.Areas.admin.Controllers
{
    using System.Drawing;
    using Eage.BPM.CoreCommon;
    using Eage.BPM.WebHelper;
    [SkipCheckLogin, SkipCheckPermiss]
    public class VcodeController : Controller
    {
        //
        // GET: /admin/Vcode/
        //
        // GET: /admin/Vcode/
        [HttpGet]
        public ActionResult Vcode()
        {

            //1.0 产生一个验证码的字符串
            string vcode = GetVcode(6);

            //2.0 将验证码存入session中
            //Session[Keys.vcode] = vcode;
            SysVisitor.Instance.Vcode = vcode;

            byte[] imgbuffer;
            //3.0 将验证码画到图片上
            using (Image img = new Bitmap(90,33))
            {
                using (Graphics g = Graphics.FromImage(img))
                {
                    g.Clear(Color.White);

                    g.DrawString(vcode, new Font("黑体", 18, FontStyle.Bold | FontStyle.Strikeout | FontStyle.Italic), new SolidBrush(Color.Red), 4, 4);
                }

                //定义一个空的内存流对象
                using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
                {
                    //将图片对象中的流写入ms中
                    img.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                    //将ms中的数据转换成byte[]
                    imgbuffer = ms.ToArray();
                }

            }

            return File(imgbuffer, "image/jpeg");
        }

        Random r = new Random();
        private string GetVcode(int p)
        {
            string str = "23456789abcdefghjkmnpqrstuvwxyzABCDEFGHJKMNPQRSTUVWXYZ";
            string res = string.Empty;
            int leng = str.Length;

            for (int i = 0; i < p; i++)
            {
                res += str[r.Next(leng)];
            }

            return res;

        }

    }
}
