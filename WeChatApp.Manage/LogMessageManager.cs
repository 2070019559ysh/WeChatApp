using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeChatApp.Core.IManage;
using WeChatApp.Core.IService;
using WetChatApp.Common;
using WetChatApp.Model;

namespace WeChatApp.Manage
{
    /// <summary>
    /// 把系统关键性日志存储到数据库
    /// </summary>
    public class LogMessageManager: ILogMessageManager
    {
        /// <summary>
        /// 依赖注入数据库记录系统日志操作实现类
        /// </summary>
        [Inject]
        public ILogMessageService logMessageService;
        private readonly LogHelper log = new LogHelper(typeof(LogMessageManager));

        /// <summary>
        /// 新增日志信息
        /// </summary>
        /// <param name="logMessage">日志信息</param>
        /// <returns>是否新增成功</returns>
        public bool InsertLog(LogMessage logMessage)
        {
            log.Info("新增日志信息到数据库InsertLog()");
            try
            {
                return logMessageService.InsertLog(logMessage);
            }
            catch (Exception ex)
            {
                log.Error("新增日志信息到数据库InsertLog()，发生异常", ex);
                return false;
            }
        }

        /// <summary>
        /// 根据Id查找日志信息
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns>日志信息</returns>
        public LogMessage SearchLog(int id)
        {
            log.Info("根据Id查找存储在数据库的日志信息SearchLog()");
            try
            {
                return logMessageService.SearchLog(id);
            }
            catch (Exception ex)
            {
                log.Error("根据Id查找存储在数据库的日志信息SearchLog()，发生异常", ex);
                return null;
            }
        }

        /// <summary>
        /// 分页查找系统日志信息
        /// </summary>
        /// <param name="fromTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="pageSize">每页最大记录数</param>
        /// <returns></returns>
        public List<LogMessage> SearchLogForPage(DateTime fromTime, DateTime endTime, int pageIndex = 1, int pageSize = 10)
        {
            log.Info("分页查找系统日志信息SearchLogForPage()");
            try
            {
                return logMessageService.SearchLogForPage(fromTime,endTime,pageIndex,pageSize);
            }
            catch (Exception ex)
            {
                log.Error("分页查找系统日志信息SearchLogForPage()，发生异常", ex);
                return null;
            }
        }
    }
}
