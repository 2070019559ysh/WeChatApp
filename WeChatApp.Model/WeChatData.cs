using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeChatApp.Model
{
    /// <summary>
    /// 存储微信数据的实体类
    /// </summary>
    public class WeChatData
    {
        /// <summary>
        /// ID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 微信数据的键（多记录中必须唯一）
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// 微信数据的值
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// 最近一次更新的时间
        /// </summary>
        public DateTime UpdateTime { get; set; }
    }
}
