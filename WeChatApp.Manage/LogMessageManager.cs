using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
        private readonly Type logType;

        public LogMessageManager(Type logType)
        {
            this.logType = logType;
        }

        /// <summary>
        /// 记录Warn级别的日志信息
        /// </summary>
        /// <param name="message">日志信息</param>
        public void Warn(string message)
        {
            LogMessage logMessage = new LogMessage()
            {
                Level = "Warn",
                Thread = Thread.CurrentThread.ManagedThreadId,
                Logger = logType.FullName,
                LogTime = DateTime.Now,
                Message = message
            };
            logMessageService.InsertLog(logMessage);
        }

        /// <summary>
        /// 记录Warn级别的日志信息
        /// </summary>
        /// <param name="message">日志信息</param>
        /// <param name="arg0">{num}参数值</param>
        public void WarnFormat(string message, object arg0)
        {
            this.Warn(string.Format(message, arg0));
        }

        /// <summary>
        /// 记录Warn级别的日志信息
        /// </summary>
        /// <param name="message">日志信息</param>
        /// <param name="args">{num}参数值</param>
        public void WarnFormat(string message, params object[] args)
        {
            this.Warn(string.Format(message, args));
        }

        /// <summary>
        /// 记录Error级别的日志信息
        /// </summary>
        /// <param name="message">日志信息</param>
        /// <param name="ex">异常对象</param>
        public void Error(string message, Exception ex = null)
        {
            LogMessage logMessage = new LogMessage()
            {
                Level = "Error",
                Thread = Thread.CurrentThread.ManagedThreadId,
                Logger = logType.FullName,
                LogTime = DateTime.Now,
                Message = message
            };
            if (ex != null)
            {
                StringBuilder logText = new StringBuilder();
                logText.AppendLine(string.Format("异常信息：{0}", ex.Message));
                logText.AppendLine(string.Format("异常堆栈：{0}", ex.StackTrace));
                if (ex.InnerException != null)
                {
                    logText.AppendLine(string.Format("异常信息：{0}", ex.InnerException.Message));
                    logText.AppendLine(string.Format("异常堆栈：{0}", ex.InnerException.StackTrace));
                }
                logMessage.ExMessage += logText;
            }
            logMessageService.InsertLog(logMessage);
        }

        /// <summary>
        /// 记录Error级别的日志信息
        /// </summary>
        /// <param name="message">日志信息</param>
        /// <param name="arg0">{num}参数值</param>
        public void ErrorFormat(string message, Exception ex = null, object arg0 = null)
        {
            this.Error(string.Format(message, arg0), ex);
        }

        /// <summary>
        /// 记录Warn级别的日志信息
        /// </summary>
        /// <param name="message">日志信息</param>
        /// <param name="args">{num}参数值</param>
        public void ErrorFormat(string message, Exception ex = null, params object[] args)
        {
            this.Error(string.Format(message, args), ex);
        }

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
