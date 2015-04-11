using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace iiceqx.Tool.IocHelper
{
    public class IocHelper
    {
        /// <summary>
        /// IOC宿主容器
        /// </summary>
        private static readonly IUnityContainer container;
        static IocHelper()
        {
            container = new UnityContainer();
            UnityConfigurationSection configuration = ConfigurationManager.GetSection(UnityConfigurationSection.SectionName) as UnityConfigurationSection;
            configuration.Configure(container, "defaultContainer");
        }

        /// <summary>
        /// 初始化Ioc容器关系
        /// </summary>
        /// <typeparam name="TInterface">接口</typeparam>
        /// <typeparam name="T">接口实现</typeparam>
        public static void UnityContainerInitialize<TInterface, T>() where T : TInterface
        {
            container.RegisterType<TInterface, T>(new ContainerControlledLifetimeManager());
        }

        /// <summary>
        /// 从IOC容器中取指定对象
        /// </summary>
        /// <returns></returns>
        public static T Resolve<T>() where T : class
        {
            var t = container.Resolve<T>();
            if (t == null)
            {
                string filename = string.Format(@"IocLog/{0}/{1}.txt", DateTime.Now.ToString("yyyyMM"), DateTime.Now.ToString("yyyyMMdd"));
                string msg = string.Format("类型{0}初始化失败", typeof(T).ToString());
                Logger.WriteFileLog(filename, "IocLog", msg);
            }
            return t;
        }

        ///// <summary>
        ///// 获取指定类型的实例化对象
        ///// </summary>
        ///// <typeparam name="T">类型</typeparam>
        ///// <returns>实例化对象</returns>
        //public static T Resolve<T>() where T : new()
        //{
        //    //ObjectFactory.Initialize(
        //    //    x =>
        //    //    {
        //    //        x.For<T>().LifecycleIs(Lifecycles.GetLifecycle(InstanceScope.PerRequest)).Use<T>();
        //    //    });
        //    return new T();
        //}
    }
}
