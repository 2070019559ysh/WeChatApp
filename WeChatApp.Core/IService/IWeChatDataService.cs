using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeChatApp.Model;

namespace WetChatApp.Core.IService
{
    /// <summary>
    /// 微信对接涉及的数据进行保存、查询等操作的接口
    /// </summary>
    public interface IWeChatDataService
    {
        /// <summary>
        /// 新增一条微信数据记录
        /// </summary>
        /// <param name="weChatData">微信数据对象</param>
        /// <returns>是否新增成功</returns>
        bool InsertWeChatData(WeChatData weChatData);
    }
}
