using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WeChatApp.Core.LinkUp;

namespace WeChatApp.Web.Controllers
{
    public class HomeController : Controller
    {
        /// <summary>
        /// 对接微信实体对象
        /// </summary>
        private readonly IConnect connectWeChat;

        public HomeController(IConnect connect)
        {
            connectWeChat = connect;
        }

        public ActionResult Index()
        {
            if (connectWeChat.CheckSignature(Request))
            {
                string echostr = Request.QueryString["echostr"];
                return Content(echostr);//如果请求来自微信服务器，原样返回echostr参数内容
            }
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}