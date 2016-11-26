using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WetChatApp.Model
{
    /// <summary>
    /// 系统日志信息存储
    /// </summary>
    public class LogMessage
    {
        /// <summary>
        /// 日志Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 记录时间
        /// </summary>
        public DateTime LogTime { get; set; }

        /// <summary>
        /// 线程Id
        /// </summary>
        public string Thread { get; set; }

        /// <summary>
        /// 日志等级，Info、Warn、Error
        /// </summary>
        public string Level { get; set; }

        /// <summary>
        /// 日志器（记录日志所在的类）
        /// </summary>
        public string Logger { get; set; }

        /// <summary>
        /// 日志的自定义信息
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 系统异常详细信息
        /// </summary>
        public string ExMessage { get; set; }
    }
}
