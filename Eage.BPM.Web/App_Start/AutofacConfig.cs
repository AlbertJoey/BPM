using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Eage.BPM.Web
{
    using Autofac;
    using Autofac.Integration.Mvc;
    using CoreCommon;
    using Eage.BPM.Cache;
    using System.Reflection;
    using System.Web.Mvc;
    

    /// <summary>
    ///利用autofac框架进行业务逻辑和数据仓储类型对象的创建
    /// </summary>
    public class AutofacConfig
    {
        public static void Register()
        {
            try
            {
                //实例化一个autofac的创建容器
                var builder = new ContainerBuilder();

                //autofac创建的控制器类存放在哪个程序集中
                Assembly controllerAss = Assembly.Load("Eage.BPM.Web");
                builder.RegisterControllers(controllerAss);

                //autofac注册数据仓储所在程序集中所有的类的对象实例
                Assembly respAss = Assembly.Load("Eage.BPM.Repository");
                //创建respAss中所有类的instance以此类的实现接口存储
                builder.RegisterTypes(respAss.GetTypes()).AsImplementedInterfaces();

                //autofac注册业务逻辑所在程序集中所有的类的对象实例
                Assembly resAss = Assembly.Load("Eage.BPM.Services");
                //创建resAss中所有类的instance以此类的实现接口存储
                builder.RegisterTypes(resAss.GetTypes()).AsImplementedInterfaces();

                //创建一个Autofac的容器
                var container = builder.Build();

                //将container对象缓存到HttpRuntime.cache中，并且是永久有效
                CacheHelper.Insert(Keys.AutofacContainer, container);

                //Resolve方法可以从autofac容器中获取指定的IsysuserInfoSercies的具体实现类的实体对象
                //container.Resolve<IsysuserInfoSercies>();

                //将MVC的控制器对象实例 交由autofac来创建
                DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            
        }
    }
}