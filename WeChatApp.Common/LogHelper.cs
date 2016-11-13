using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WetChatApp.Common
{
    /// <summary>
    /// 写日志到文件的帮助类
    /// </summary>
    public class LogHelper:IDisposable
    {
        /// <summary>
        /// 日志文件根路径
        /// </summary>
        private static string logPath;
        /// <summary>
        /// 当前最新的日志文件路径
        /// </summary>
        private static string updateLogPath;
        /// <summary>
        /// 当前实例记录日志所在的类信息
        /// </summary>
        private readonly Type logType;
        /// <summary>
        /// 日志级别Info的文件流
        /// </summary>
        private static FileStream infoFileStream;
        /// <summary>
        /// 日志级别Info的文件信息写入器
        /// </summary>
        private static StreamWriter infoStreamWriter;
        /// <summary>
        /// 日志级别Warn的文件流
        /// </summary>
        private static FileStream warnFileStream;
        /// <summary>
        /// 日志级别Warn的文件信息写入器
        /// </summary>
        private static StreamWriter warnStreamWriter;
        /// <summary>
        /// 日志级别Error的文件流
        /// </summary>
        private static FileStream errorFileStream;
        /// <summary>
        /// 日志级别Error的文件信息写入器
        /// </summary>
        private static StreamWriter errorStreamWriter;
        /// <summary>
        /// 所有日志级别的文件流
        /// </summary>
        private static FileStream allFileStream;
        /// <summary>
        /// 所有日志级别的文件信息写入器
        /// </summary>
        private static StreamWriter allStreamWriter;

        public LogHelper(Type logType)
        {
            logPath = ConfigurationManager.AppSettings["LogPath"];//获取配置文件中指定的Log文件路径
            logPath = GetExistsPath(logPath);
            UpdateFileStream();
            this.logType = logType;
        }

        /// <summary>
        /// 更新写日志的文件流，使用当天的日志文件
        /// </summary>
        private void UpdateFileStream()
        {
            DateTime nowTime = DateTime.Now;
            string filePath = string.Format(logPath + "/{0}/{1}/", nowTime.ToString("yyyy-M"), nowTime.ToString("yyyy-M-d"));
            bool fileObjNull = infoFileStream == null || infoStreamWriter == null 
                || warnFileStream == null || warnStreamWriter == null 
                || errorFileStream == null || errorStreamWriter == null 
                || allFileStream == null || allStreamWriter == null;
            if (!filePath.Equals(updateLogPath) || fileObjNull)
            {
                updateLogPath = filePath;
                Directory.CreateDirectory(updateLogPath);
                if (infoFileStream != null)
                {
                    infoStreamWriter.Close();
                    infoFileStream.Close();
                }
                infoFileStream = new FileStream(updateLogPath + "InfoLog.log", FileMode.Append);
                infoStreamWriter = new StreamWriter(infoFileStream, Encoding.Default);
                if (warnFileStream != null)
                {
                    warnStreamWriter.Close();
                    warnFileStream.Close();
                }
                warnFileStream = new FileStream(updateLogPath + "WarnLog.log", FileMode.Append);
                warnStreamWriter = new StreamWriter(warnFileStream, Encoding.Default);
                if (errorFileStream != null)
                {
                    errorStreamWriter.Close();
                    errorFileStream.Close();
                }
                errorFileStream = new FileStream(updateLogPath + "ErrorLog.log", FileMode.Append);
                errorStreamWriter = new StreamWriter(errorFileStream, Encoding.Default);
                if (allFileStream != null)
                {
                    allStreamWriter.Close();
                    allFileStream.Close();
                }
                allFileStream = new FileStream(updateLogPath + "AllLog.log", FileMode.Append);
                allStreamWriter = new StreamWriter(allFileStream, Encoding.Default);
            }
        }

        /// <summary>
        /// 根据分配的一个系统路径，确认出一个真实存在的系统路径
        /// </summary>
        /// <param name="path">系统路径</param>
        /// <returns>真实存在的系统路径</returns>
        private string GetExistsPath(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                //不存在配置的Log文件路径，尝试获取一个存在的磁盘路径
                string[] initPaths = { "D:\\", "E:\\", "F:\\", "C:\\" };
                foreach (var initPath in initPaths)
                {
                    if (!Directory.Exists(initPath))
                    {
                        path = initPath + "WeChatAppLog\\";
                        break;
                    }
                }
            }
            Directory.CreateDirectory(path);
            return path;
        }

        /// <summary>
        /// 记录Info级别的日志信息
        /// </summary>
        /// <param name="message">日志信息</param>
        public void Info(string message)
        {
            UpdateFileStream();
            infoStreamWriter.WriteLine("\r\n线程ID：{0}", Thread.CurrentThread.ManagedThreadId);
            infoStreamWriter.WriteLine("时间：{0}", DateTime.Now);
            infoStreamWriter.WriteLine("错误等级：Info");
            infoStreamWriter.WriteLine("类的信息：{0}", logType.FullName);
            infoStreamWriter.WriteLine("描述信息：{0}", message);
            infoStreamWriter.Flush();
            allStreamWriter.WriteLine("\r\n线程ID：{0}", Thread.CurrentThread.ManagedThreadId);
            allStreamWriter.WriteLine("时间：{0}", DateTime.Now);
            allStreamWriter.WriteLine("错误等级：Info");
            allStreamWriter.WriteLine("类的信息：{0}", logType.FullName);
            allStreamWriter.WriteLine("描述信息：{0}", message);
            allStreamWriter.Flush();
        }

        /// <summary>
        /// 记录Info级别的日志信息
        /// </summary>
        /// <param name="message">日志信息</param>
        /// <param name="arg0">{num}参数值</param>
        public void InfoFormat(string message, object arg0)
        {
            this.Info(string.Format(message, arg0));
        }

        /// <summary>
        /// 记录Info级别的日志信息
        /// </summary>
        /// <param name="message">日志信息</param>
        /// <param name="args">{num}参数值</param>
        public void InfoFormat(string message,params object[] args)
        {
            this.Info(string.Format(message, args));
        }

        /// <summary>
        /// 记录Warn级别的日志信息
        /// </summary>
        /// <param name="message">日志信息</param>
        public void Warn(string message)
        {
            UpdateFileStream();
            warnStreamWriter.WriteLine("\r\n线程ID：{0}", Thread.CurrentThread.ManagedThreadId);
            warnStreamWriter.WriteLine("时间：{0}", DateTime.Now);
            warnStreamWriter.WriteLine("错误等级：Warn");
            warnStreamWriter.WriteLine("类的信息：{0}", logType.FullName);
            warnStreamWriter.WriteLine("描述信息：{0}", message);
            warnStreamWriter.Flush();
            allStreamWriter.WriteLine("\r\n线程ID：{0}", Thread.CurrentThread.ManagedThreadId);
            allStreamWriter.WriteLine("时间：{0}", DateTime.Now);
            allStreamWriter.WriteLine("错误等级：Warn");
            allStreamWriter.WriteLine("类的信息：{0}", logType.FullName);
            allStreamWriter.WriteLine("描述信息：{0}", message);
            allStreamWriter.Flush();
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
        public void Error(string message,Exception ex=null)
        {
            UpdateFileStream();
            errorStreamWriter.WriteLine("\r\n线程ID：{0}", Thread.CurrentThread.ManagedThreadId);
            errorStreamWriter.WriteLine("时间：{0}", DateTime.Now);
            errorStreamWriter.WriteLine("错误等级：Error");
            errorStreamWriter.WriteLine("类的信息：{0}", logType.FullName);
            errorStreamWriter.WriteLine("描述信息：{0}", message);
            allStreamWriter.WriteLine("\r\n线程ID：{0}", Thread.CurrentThread.ManagedThreadId);
            allStreamWriter.WriteLine("时间：{0}", DateTime.Now);
            allStreamWriter.WriteLine("错误等级：Error");
            allStreamWriter.WriteLine("类的信息：{0}", logType.FullName);
            allStreamWriter.WriteLine("描述信息：{0}", message);
            if (ex != null)
            {
                errorStreamWriter.WriteLine("异常信息：{0}", ex.Message);
                errorStreamWriter.WriteLine("异常堆栈：{0}", ex.StackTrace);
                allStreamWriter.WriteLine("异常信息：{0}", ex.Message);
                allStreamWriter.WriteLine("异常堆栈：{0}", ex.StackTrace);
                if (ex.InnerException != null)
                {
                    errorStreamWriter.WriteLine("内部异常信息：{0}", ex.InnerException.Message);
                    errorStreamWriter.WriteLine("内部异常堆栈：{0}", ex.InnerException.StackTrace);
                    allStreamWriter.WriteLine("内部异常信息：{0}", ex.InnerException.Message);
                    allStreamWriter.WriteLine("内部异常堆栈：{0}", ex.InnerException.StackTrace);
                }
            }
            errorStreamWriter.Flush();
            allStreamWriter.Flush();
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
        /// 系统垃圾回收时释放文件流资源
        /// </summary>
        public void Dispose()
        {
            infoStreamWriter.Dispose();
            infoStreamWriter.Close();
            infoFileStream.Dispose();
            infoFileStream.Close();
            warnStreamWriter.Dispose();
            warnStreamWriter.Close();
            warnFileStream.Dispose();
            warnFileStream.Close();
            errorStreamWriter.Dispose();
            errorStreamWriter.Close();
            errorFileStream.Dispose();
            errorFileStream.Close();
            allStreamWriter.Dispose();
            allStreamWriter.Close();
            allFileStream.Dispose();
            allFileStream.Close();
        }
    }
}
