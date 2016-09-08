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

namespace WeChatApp.LinkUp
{
    /// <summary>
    /// 微信接入类
    /// </summary>
    public class Connect:IConnect
    {
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

        public string GetAccessToken()
        {
            string appId = ConfigurationManager.AppSettings["AppID"];
            string secret = ConfigurationManager.AppSettings["AppSecret"];
            string url = String.Format("https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid={0}&secret={1}", appId, secret);
            string resultStr = SimulateRequest.HttpGet(url);

        }
    }
}
