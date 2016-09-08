using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WeChatApp.Web.Startup))]
namespace WeChatApp.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
