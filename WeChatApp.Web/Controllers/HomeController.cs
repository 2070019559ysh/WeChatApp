﻿using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WeChatApp.Core.ILinkUp;
using WetChatApp.Common;

namespace WeChatApp.Web.Controllers
{
    public class HomeController : Controller
    {
        /// <summary>
        /// 对接微信实体对象
        /// </summary>
        private readonly IConnect connectWeChat;
        private readonly LogHelper log=new LogHelper(typeof(HomeController));

        public HomeController(IConnect connect)
        {
            connectWeChat = connect;
        }

        public ActionResult Index()
        {
            log.Info("进入Home/Index");
            if (connectWeChat.CheckSignature(Request))
            {
                log.Info("是微信请求Home/Index");
                string echostr = Request.QueryString["echostr"];
                return Content(echostr);//如果请求来自微信服务器，原样返回echostr参数内容
            }
            log.Info("测试Warn日志");
            log.Info("测试Error日志");
            //connectWeChat.GetAccessToken();
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