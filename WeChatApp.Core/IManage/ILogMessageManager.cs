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
        /// 记录Warn级别的日志信息
        /// </summary>
        /// <param name="message">日志信息</param>
        void Warn(string message);

        /// <summary>
        /// 记录Warn级别的日志信息
        /// </summary>
        /// <param name="message">日志信息</param>
        /// <param name="arg0">{num}参数值</param>
        void WarnFormat(string message, object arg0);

        /// <summary>
        /// 记录Warn级别的日志信息
        /// </summary>
        /// <param name="message">日志信息</param>
        /// <param name="args">{num}参数值</param>
        void WarnFormat(string message, params object[] args);

        /// <summary>
        /// 记录Error级别的日志信息
        /// </summary>
        /// <param name="message">日志信息</param>
        /// <param name="ex">异常对象</param>
        void Error(string message, Exception ex = null);

        /// <summary>
        /// 记录Error级别的日志信息
        /// </summary>
        /// <param name="message">日志信息</param>
        /// <param name="arg0">{num}参数值</param>
        void ErrorFormat(string message, Exception ex = null, object arg0 = null);

        /// <summary>
        /// 记录Warn级别的日志信息
        /// </summary>
        /// <param name="message">日志信息</param>
        /// <param name="args">{num}参数值</param>
        void ErrorFormat(string message, Exception ex = null, params object[] args);

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
