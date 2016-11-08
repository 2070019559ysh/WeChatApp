using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace WeChatApp.Core.ILinkUp
{
    /// <summary>
    /// 接入微信的接口
    /// </summary>
    public interface IConnect
    {
        /// <summary>
        /// 验证请求中的签名，确认请求是否来自微信
        /// </summary>
        /// <param name="request">当前请求对象</param>
        /// <returns>请求是否来自微信</returns>
        bool CheckSignature(HttpRequestBase request);

        /// <summary>
        /// 获取公众号的AccessToken
        /// </summary>
        /// <returns>微信提供的AccessToken</returns>
        string GetAccessToken();
    }
}
