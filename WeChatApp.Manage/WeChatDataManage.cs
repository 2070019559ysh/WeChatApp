using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeChatApp.Model;
using WeChatApp.Core.IManage;
using WeChatApp.Core.IService;
using Ninject;
using WetChatApp.Common;

namespace WeChatApp.Manage
{
    /// <summary>
    /// 微信数据表的管理类
    /// </summary>
    public class WeChatDataManage:IWeChatDataManage
    {

        /// <summary>
        /// 依赖注入的微信数据表操作接口
        /// </summary>
        [Inject]
        public IWeChatDataService WeChatDataService { get; set; }

        /// <summary>
        /// 新增一条微信数据记录
        /// </summary>
        /// <param name="weChatData">微信数据对象</param>
        /// <returns>是否新增成功</returns>
        public bool InsertWeChatData(WeChatData weChatData)
        {
            try
            {
                new LogHelper(this.GetType()).InfoFormat("新增一条微信数据记录InsertWeChatData((weChatData.Key:{0},weChatData.Value:{1})", weChatData.Key, weChatData.Value);
                weChatData.UpdateTime = DateTime.Now;
                return WeChatDataService.InsertWeChatData(weChatData);
            }
            catch (Exception ex)
            {
                new LogHelper(this.GetType()).Error("新增一条微信数据记录InsertWeChatData(),发生异常",ex);
                return false;
            }
        }

        /// <summary>
        /// 添加或更新微信数据记录，不存在同Key则添加，存在则更新
        /// </summary>
        /// <param name="weChatData">微信数据对象</param>
        /// <returns>是否新增或更新成功</returns>
        public bool InsertOrUpdateWeChatData(WeChatData weChatData)
        {
            try
            {
                new LogHelper(this.GetType()).InfoFormat("添加或更新微信数据记录InsertOrUpdateWeChatData(weChatData.Key:{0},weChatData.Value:{1})", weChatData.Key, weChatData.Value);
                weChatData.UpdateTime = DateTime.Now;
                return WeChatDataService.InsertOrUpdateWeChatData(weChatData);
            }
            catch (Exception ex)
            {
                new LogHelper(this.GetType()).Error("添加或更新微信数据记录InsertOrUpdateWeChatData(),发生异常", ex);
                return false;
            }
        }

        /// <summary>
        /// 根据微信数据Id查找微信数据对象
        /// </summary>
        /// <param name="id">微信数据Id</param>
        /// <returns>微信数据对象</returns>
        public WeChatData SearchWeChatData(int id)
        {
            try
            {
                new LogHelper(this.GetType()).InfoFormat("Manage,根据微信数据Id查找微信数据对象_SearchWeChatData(id:{0})", id);
                return WeChatDataService.SearchWeChatData(id);
            }
            catch (Exception ex)
            {
                new LogHelper(this.GetType()).Error("Manage,根据微信数据Id查找微信数据对象_SearchWeChatData(),发生异常",ex);
                return null;
            }
        }

        /// <summary>
        /// 根据微信数据的Key查找微信数据对象
        /// </summary>
        /// <param name="key">微信数据的Key</param>
        /// <returns>微信数据对象</returns>
        public WeChatData SearchWeChatData(string key)
        {
            try
            {
                new LogHelper(this.GetType()).InfoFormat("Manage,根据微信数据的Key查找微信数据对象_SearchWeChatData(key:{0})", key);
                return WeChatDataService.SearchWeChatData(key);
            }
            catch (Exception ex)
            {
                new LogHelper(this.GetType()).Error("Manage,根据微信数据的Key查找微信数据对象_SearchWeChatData(),发生异常", ex);
                return null;
            }
        }
    }
}
