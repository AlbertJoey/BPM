//------------------------------------------------------------------------------
// <auto-generated>
//    此代码是根据模板生成的。
//
//    手动更改此文件可能会导致应用程序中发生异常行为。
//    如果重新生成代码，则将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace Eage.BPM.IServices
{
    using System;
    using System.Collections.Generic;
     
    using IServices;
    using Model;
    /// <summary>
    /// 针对此表的特殊业务操作的约定
    /// By: Albert.joey 20151203
    /// </summary>
    public partial interface ISys_RoleNavBtnsService :IBaseServices<Sys_RoleNavBtns>
    {
        #region 针对此表的特殊业务操作的约定

        //跟对userid获取相应权限的约定
        List<Usp_GetNavAndNavbuttonPermissByUserID_Result> GetPermissForUserByCache(int userid);
        #endregion
    }
}
