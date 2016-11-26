using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WeChatApp.Service;
using WetChatApp.Common;
using WetChatApp.Model;

namespace WetChatApp.Service
{
    /// <summary>
    /// 数据库记录系统日志，日志的数据操作实现类
    /// </summary>
    public class LogMessageService
    {
        private SqlHelper sqlHelper = new SqlHelper();
        private readonly LogHelper log = new LogHelper(typeof(LogMessageService));

        /// <summary>
        /// 新增日志信息
        /// </summary>
        /// <param name="logMessage">日志信息</param>
        /// <returns>是否新增成功</returns>
        public bool InsertLog(LogMessage logMessage)
        {
            string sql = "INSERT INTO LogMessage(Thread,[Level],Logger,[Message],ExMessage) VALUES(@Thread,@Level,@Logger,@Message,@ExMessage)";
            SqlParameter[] paras = new SqlParameter[]
            {
                new SqlParameter("@Thread",Thread.CurrentThread.ManagedThreadId),
                new SqlParameter("@Level",logMessage.Level),
                new SqlParameter("@Logger",logMessage.Logger),
                new SqlParameter("@Message",logMessage.Message),
                new SqlParameter("@ExMessage",logMessage.ExMessage)
            };
            int row = sqlHelper.ExecuteNonQuery(sql, paras);
            return row > 0;
        }

        public LogMessage SearchLog(int id)
        {
            LogMessage logMessage = null;
            string sql = "SELECT [Id],[LogTime],Thread,[Level],Logger,[Message],ExMessage FROM LogMessage WHERE Id=@Id";
            SqlParameter para = new SqlParameter("@Id", id);
            SqlDataReader dataReader = sqlHelper.ExecuteReader(sql, new SqlParameter[] { para });
            if (dataReader.Read())
            {
                logMessage = new LogMessage();
                logMessage.Id = Convert.ToInt32(dataReader["Id"]);
                logMessage.LogTime = Convert.ToDateTime(dataReader["LogTime"]);
                logMessage.Thread = dataReader["Thread"].ToString();
                logMessage.Level = dataReader["Level"].ToString();
                logMessage.Logger = dataReader["Logger"].ToString();
                logMessage.Message = dataReader["Message"].ToString();
                logMessage.ExMessage = dataReader["ExMessage"] == null ? null : dataReader["ExMessage"].ToString();
            }
            return logMessage;
        }
    }
}
