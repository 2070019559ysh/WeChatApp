using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeChatApp.Model;

namespace WeChatApp.Core.IService
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

        /// <summary>
        /// 添加或更新微信数据记录，不存在同Key则添加，存在则更新
        /// </summary>
        /// <param name="weChatData">微信数据对象</param>
        /// <returns>是否新增或更新成功</returns>
        bool InsertOrUpdateWeChatData(WeChatData weChatData);

        /// <summary>
        /// 根据Id或Key更新微信数据记录
        /// </summary>
        /// <param name="weChatData">微信数据对象</param>
        /// <returns>是否更新成功</returns>
        bool UpdateWeChatData(WeChatData weChatData);

        /// <summary>
        /// 根据微信数据Id查找微信数据对象
        /// </summary>
        /// <param name="id">微信数据Id</param>
        /// <returns>微信数据对象</returns>
        WeChatData SearchWeChatData(Guid id);

        /// <summary>
        /// 根据微信数据的Key查找微信数据对象
        /// </summary>
        /// <param name="id">微信数据的Key</param>
        /// <returns>微信数据对象</returns>
        WeChatData SearchWeChatData(string key);
    }
}
