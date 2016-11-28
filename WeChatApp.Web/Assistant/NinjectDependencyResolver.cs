using Ninject;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using WeChatApp.Core.ILinkUp;
using WeChatApp.Core.IManage;
using WeChatApp.Core.IService;
using WeChatApp.LinkUp;
using WeChatApp.Manage;
using WeChatApp.Service;

namespace WeChatApp.Web.Assistant
{
    /// <summary>
    /// 定义基于Ninject（依赖注入）的可简化服务位置和依赖关系解析的方法
    /// </summary>
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private IKernel kernel;

        /// <summary>
        /// 实例化基于Ninject（依赖注入）的对象
        /// </summary>
        public NinjectDependencyResolver()
        {
            kernel = new StandardKernel();
            this.AddBinding();
        }

        /// <summary>
        /// 添加依赖注入的绑定项
        /// </summary>
        private void AddBinding()
        {
            #region ILinkUp的依赖注入
            kernel.Bind<IConnect>().To<Connect>();
            #endregion

            #region IManage的依赖注入
            kernel.Bind<IWeChatDataManage>().To<WeChatDataManage>();
            kernel.Bind<ILogMessageManager>().To<LogMessageManager>();
            #endregion

            #region ISerice的依赖注入
            kernel.Bind<IWeChatDataService>().To<WeChatDataService>();
            kernel.Bind<ILogMessageService>().To<LogMessageService>();
            #endregion
        }

        /// <summary>
        /// 解析支持任意对象创建的一次注册的服务
        /// </summary>
        /// <param name="serviceType">所请求的服务或对象的类型</param>
        /// <returns>请求的服务或对象</returns>
        public object GetService(Type serviceType)
        {
            return this.kernel.TryGet(serviceType);
        }

        /// <summary>
        /// 解析多次注册的服务
        /// </summary>
        /// <param name="serviceType">所请求的服务的类型</param>
        /// <returns>请求的服务</returns>
        public IEnumerable<object> GetServices(Type serviceType)
        {
            return this.kernel.GetAll(serviceType);
        }

        public void Dispose()
        {
            //throw new NotImplementedException();
        }
    }
}