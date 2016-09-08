using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;

namespace WeChatApp.Common
{
    /// <summary>
    /// 模拟请求并获取响应出来对象
    /// </summary>
    public class SimulateRequest
    {
        /// <summary>
        /// 发送Post请求并返回响应字符串
        /// </summary>
        /// <param name="Url">请求Url，如：http://yshweb.wicp.net/ </param>
        /// <param name="postDataStr">请求参数，如：key=value&key2=value2 或{key:value,key2:value2}</param>
        /// <returns>响应字符串</returns>
        public static string HttpPost(string Url, string postDataStr)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = Encoding.UTF8.GetByteCount(postDataStr);
            Stream myRequestStream = request.GetRequestStream();
            StreamWriter myStreamWriter = new StreamWriter(myRequestStream, Encoding.GetEncoding("UTF-8"));
            myStreamWriter.Write(postDataStr);
            myStreamWriter.Dispose();
            myRequestStream.Dispose();
            return GetResponse(request);
        }
 
        /// <summary>
        /// 发送Get请求并返回响应字符串
        /// </summary>
        /// <param name="Url">请求Url，如：http://yshweb.wicp.net/Home?lg=zh-cn </param>
        /// <returns>响应字符串</returns>
        public static string HttpGet(string Url)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);
            request.Method = "GET";
            request.ContentType = "text/html;charset=UTF-8";
            return GetResponse(request);
        }

        /// <summary>
        /// 根据HttpWebRequest请求对象获取最终响应信息
        /// </summary>
        /// <param name="request">HttpWebRequest请求对象</param>
        /// <returns>响应字符串信息</returns>
        private static string GetResponse(HttpWebRequest request)
        {
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream myResponseStream = response.GetResponseStream();
            StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("UTF-8"));
            string retString = myStreamReader.ReadToEnd();
            myStreamReader.Dispose();
            myResponseStream.Dispose();
            return retString;
        }
    }
}