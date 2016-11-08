using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using WeChatApp.Common;
using WeChatApp.Common.Extension;
using WeChatApp.Core.ILinkUp;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using WeChatApp.Core.IManage;
using WeChatApp.Model;
using Ninject;
using WetChatApp.LinkUp.HelpModel;
using log4net;

namespace WeChatApp.LinkUp
{
    /// <summary>
    /// 微信接入类
    /// </summary>
    public class Connect:IConnect
    {
        private readonly ILog logObj = LogManager.GetLogger(typeof(Connect));

        /// <summary>
        /// 依赖注入的微信数据表管理接口
        /// </summary>
        [Inject]
        public IWeChatDataManage WeChatDataManage { get; set; }

        /// <summary>
        /// 验证请求中的签名，确认请求是否来自微信
        /// </summary>
        /// <param name="request">当前请求对象</param>
        /// <returns>请求是否来自微信</returns>
        public bool CheckSignature(HttpRequestBase request)
        {
            string signature = request.QueryString["signature"];
            string timestamp = request.QueryString["timestamp"];
            string nonce = request.QueryString["nonce"];
            if (String.IsNullOrEmpty(signature) || String.IsNullOrEmpty(timestamp) || String.IsNullOrEmpty(nonce))
            {
                return false;
            }
            string token = ConfigurationManager.AppSettings["Token"];
            string[] strs = { token, timestamp, nonce };
            Array.Sort(strs);
            string joinStr = string.Join("", strs);
            //List<string> strList = new List<string>() { token, timestamp, nonce };
            //strList.Sort();
            //string joinStr = String.Empty;
            //strList.ForEach(str => joinStr += str);
            string sha1Str = joinStr.SHA1Encrypt();
            return sha1Str.Equals(signature) ? true : false;
        }

        /// <summary>
        /// 获取公众号的AccessToken
        /// </summary>
        /// <returns>微信提供的AccessToken</returns>
        public string GetAccessToken()
        {
            logObj.Info("获取公众号的AccessToken,GetAccessToken()");
            string accessToken = String.Empty;
            //先从数据库获取Access_Token，如果不存在或已过期，则重新跟微信拿Access_Token
            WeChatData wechatData = WeChatDataManage.SearchWeChatData("AccessToken");
            if (wechatData != null)
            {
                logObj.Info("获取公众号的AccessToken,GetAccessToken(),数据库存在微信数据Key:AccessToken");
                var wechatDataObj = JsonConvert.DeserializeObject<JObject>(wechatData.Value);
                var overdueTime = wechatData.UpdateTime.AddSeconds(int.Parse(wechatDataObj["expires_in"].ToString()));//access_token到期时间
                if (DateTime.Now >= overdueTime)
                {
                    logObj.Info("获取公众号的AccessToken,GetAccessToken(),数据库的微信数据Key:AccessToken已过期");
                    accessToken = GetWeixinAccessToken();
                }
                else
                {
                    logObj.Info("获取公众号的AccessToken,GetAccessToken(),等到有效的微信数据Key:AccessToken");
                    accessToken = wechatDataObj["access_token"].ToString();
                }
            }
            else
            {
                logObj.Info("获取公众号的AccessToken,GetAccessToken(),数据库不存在微信数据Key:AccessToken，直接去微信获取AccessToken");
                accessToken = GetWeixinAccessToken();
            }
            logObj.Info("获取公众号的AccessToken,GetAccessToken(),返回：" + accessToken);
            return accessToken;
        }

        /// <summary>
        /// 直接去微信获取AccessToken
        /// </summary>
        /// <returns>微信提供的AccessToken</returns>
        private string GetWeixinAccessToken()
        {
            logObj.Info("直接去微信获取AccessToken,GetWeixinAccessToken()");
            string accessToken = String.Empty;
            string appId = ConfigurationManager.AppSettings["AppID"];
            string secret = ConfigurationManager.AppSettings["AppSecret"];
            string url = String.Format("https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid={0}&secret={1}", appId, secret);
            string resultStr = SimulateRequest.HttpGet(url);
            var resultObj = JsonConvert.DeserializeObject<JObject>(resultStr);
            if (resultObj["errcode"] != null)
            {
                logObj.Info("直接去微信获取AccessToken,GetWeixinAccessToken()，微信服务报错");
                var errorCode = new WeixinErrorInfo(resultObj["errcode"].ToString());
                logObj.Warn(string.Format("{0}【errmsg：{1}】", errorCode.ToString(), resultObj["errmsg"].ToString()));
            }
            else
            {
                accessToken = resultObj["access_token"].ToString();
                logObj.InfoFormat("直接去微信获取AccessToken,GetWeixinAccessToken()，微信access_token:{0}", accessToken);
            }
            WeChatDataManage.InsertWeChatData(new WeChatData()
            {
                Key = "AccessToken",
                Value = resultStr
            });
            logObj.Info("直接去微信获取AccessToken,GetWeixinAccessToken(),返回：" + accessToken);
            return accessToken;
        }
    }
}
