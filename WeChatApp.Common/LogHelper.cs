using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WetChatApp.Common
{
    public class LogHelper:IDisposable
    {
        private readonly string logPath;
        private readonly Type logType;
        private static FileStream fileStream;

        public LogHelper(Type logType)
        {
            DateTime nowTime = DateTime.Now;
            string filePath = string.Format(logPath + "/{0}/{1}/", DateTime.Now.ToString("yyyy-M"), DateTime.Now.ToString("yyyy-M-d"));
            this.logType = logType;
            if (fileStream == null)
                fileStream = new FileStream(filePath + "AllLog.log", FileMode.Append);
        }

        public void Info(string message)
        {
            StreamWriter streamWriter = new StreamWriter(fileStream, Encoding.Default);
            streamWriter.WriteLine("线程ID：{0}", Thread.CurrentThread.ManagedThreadId);
            streamWriter.WriteLine("时间：{0}", DateTime.Now);
            streamWriter.WriteLine("错误等级：Info");
            streamWriter.WriteLine("描述信息：{0}", message);
            streamWriter.WriteLine("类的信息：{0}", logType.Name);
            streamWriter.Close();
        }

        public void InfoFormat(string message, object arg0)
        {
            this.Info(string.Format(message, arg0));
        }

        public void InfoFormat(string message,params object[] args)
        {
            this.Info(string.Format(message, args));
        }

        public void Error(string message,Exception ex=null)
        {
            StreamWriter streamWriter = new StreamWriter(fileStream, Encoding.Default);
            streamWriter.WriteLine("线程ID：{0}", Thread.CurrentThread.ManagedThreadId);
            streamWriter.WriteLine("时间：{0}", DateTime.Now);
            streamWriter.WriteLine("错误等级：Error");
            streamWriter.WriteLine("描述信息：{0}", message);
            if (ex != null)
            {
                streamWriter.WriteLine("异常信息：{0}", ex.Message);
                streamWriter.WriteLine("异常堆栈：{0}", ex.StackTrace);
            }
            streamWriter.WriteLine("类的信息：{0}", logType.Name);
            streamWriter.Close();
        }

        public void Dispose()
        {
            fileStream.Dispose();
            fileStream.Close();
        }
    }
}
