using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using WeChatApp.Web.Assistant;

namespace WeChatApp.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        /// <summary>
        /// 项目目的：
        /// 对接微信公众号，实现一个让用户享受接口服务的公众号平台
        /// 接口服务对接来源，聚合数据网https://www.juhe.cn/docs/index/sortby/4
        /// 微信消息，用户输入对应字符，提供对应接口服务
        /// 用户访问本接口服务网站，享受接口服务
        /// 对于收费的接口，本系统向用户收取一定接口服务享受费用
        /// </summary>
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            //注册依赖关系解析程序
            DependencyResolver.SetResolver(new NinjectDependencyResolver());
        }
    }
}
