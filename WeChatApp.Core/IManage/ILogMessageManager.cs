using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WetChatApp.Model;

namespace WeChatApp.Core.IManage
{
    /// <summary>
    /// 把系统关键性日志存储到数据库操作接口
    /// </summary>
    public interface ILogMessageManager
    {
        /// <summary>
        /// 新增日志信息
        /// </summary>
        /// <param name="logMessage">日志信息</param>
        /// <returns>是否新增成功</returns>
        bool InsertLog(LogMessage logMessage);

        /// <summary>
        /// 根据Id查找日志信息
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns>日志信息</returns>
        LogMessage SearchLog(int id);

        /// <summary>
        /// 分页查找系统日志信息
        /// </summary>
        /// <param name="fromTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="pageSize">每页最大记录数</param>
        /// <returns></returns>
        List<LogMessage> SearchLogForPage(DateTime fromTime, DateTime endTime, int pageIndex = 1, int pageSize = 10);
    }
}
