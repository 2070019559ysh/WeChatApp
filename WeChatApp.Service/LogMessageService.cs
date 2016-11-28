using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WeChatApp.Service;
using WeChatApp.Core.IService;
using WetChatApp.Model;

namespace WeChatApp.Service
{
    /// <summary>
    /// 数据库记录系统日志，日志的数据操作实现类
    /// </summary>
    public class LogMessageService: ILogMessageService
    {
        private SqlHelper sqlHelper = new SqlHelper();

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

        /// <summary>
        /// 根据Id查找日志信息
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns>日志信息</returns>
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
            dataReader.Close();
            return logMessage;
        }

        /// <summary>
        /// 分页查找系统日志信息
        /// </summary>
        /// <param name="fromTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="pageSize">每页最大记录数</param>
        /// <returns></returns>
        public List<LogMessage> SearchLogForPage(DateTime fromTime,DateTime endTime,int pageIndex=1,int pageSize=10)
        {
            
            List<LogMessage> logMessageList = new List<LogMessage>();
            string sql = @"SELECT TOP @pageSize Id, LogTime, Thread, [Level], Logger, [Message], ExMessage FROM LogMessage 
                            WHERE Id NOT IN(
                                SELECT TOP ((@pageIndex-1)*@pageSize) Id FROM LogMessage
                                WHERE LogTime >= @fromTime AND LogTime < @endTime
                                Order by LogTime DESC
                            ) Order by LogTime DESC";
            SqlParameter[] paras = new SqlParameter[]
            {
                new SqlParameter("@pageIndex",pageIndex),
                new SqlParameter("@pageSize",pageSize),
                new SqlParameter("@fromTime",fromTime),
                new SqlParameter("@endTiime",endTime),
            };
            SqlDataReader dataReader = sqlHelper.ExecuteReader(sql, paras);
            while (dataReader.Read())
            {
                LogMessage logMessage = new LogMessage();
                logMessage = new LogMessage();
                logMessage.Id = Convert.ToInt32(dataReader["Id"]);
                logMessage.LogTime = Convert.ToDateTime(dataReader["LogTime"]);
                logMessage.Thread = dataReader["Thread"].ToString();
                logMessage.Level = dataReader["Level"].ToString();
                logMessage.Logger = dataReader["Logger"].ToString();
                logMessage.Message = dataReader["Message"].ToString();
                logMessage.ExMessage = dataReader["ExMessage"] == null ? null : dataReader["ExMessage"].ToString();
                logMessageList.Add(logMessage);
            }
            dataReader.Close();
            return logMessageList;
        }
    }
}
