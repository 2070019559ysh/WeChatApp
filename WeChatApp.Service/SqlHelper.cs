using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace WeChatApp.Service
{
    /// <summary>
    /// 数据库连接、操作数据的帮助类
    /// </summary>
    public class SqlHelper
    {
        /// <summary>
        /// 连接字符串
        /// </summary>
        private static  readonly string constr=ConfigurationManager.ConnectionStrings["WeChatConnection"].ConnectionString;
        //使用连接池进行连接数据库，数据集
        private DataSet dataSet = new DataSet("DataSet");
        //数据适配器
        private SqlDataAdapter dataAdapter = new SqlDataAdapter();
        //数据库连接
        private SqlConnection con = new SqlConnection(constr);
        //涉及事务的sqlcommand
        private SqlCommand cmd = new SqlCommand();

        #region 打开数据库连接,如果已经打开则不再打开
        /// <summary>
        /// 打开数据库连接,如果已经打开则不再打开
        /// </summary>
        private void OpenConnection()
        {
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            if (con.State == ConnectionState.Broken)
            {
                con.Close();
                con.Open();
            }
        }
        #endregion

        #region 对连接执行T-SQL语句并返回受影响的行数
        /// <summary>
        /// 对连接执行T-SQL语句并返回受影响的行数
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="paras">sql参数</param>
        /// <returns>受影响的行数</returns>
        public int ExecuteNonQuery(string sql,SqlParameter[] paras)
        {
            try
            {
                return ExecuteNonQuery(sql, paras, CommandType.Text);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 对连接执行T-SQL语句并返回受影响的行数，明确涉及事务
        /// <summary>
        /// 对连接执行T-SQL语句并返回受影响的行数，明确涉及事务
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="paras">sql参数</param>
        /// <param name="isCommit">是否提交事务</param>
        /// <returns>受影响的行数</returns>
        public int ExecuteNonQuery(string sql, SqlParameter[] paras,bool isCommit)
        {
            try
            {
                return ExecuteNonQuery(sql, paras, CommandType.Text, isCommit);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 对连接执行T-SQL语句并返回受影响的行数，可以使用存储过程，明确涉及事务
        /// <summary>
        /// 对连接执行T-SQL语句并返回受影响的行数，明确涉及事务
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="paras">sql参数</param>
        /// <param name="cmdType">命令类型</param>
        /// <param name="isCommit">是否提交事务</param>
        /// <returns>受影响的行数</returns>
        public int ExecuteNonQuery(string sql, SqlParameter[] paras,CommandType cmdType, bool isCommit)
        {
            cmd.CommandType = cmdType;
            cmd.CommandText = sql;
            cmd.Connection = con;
            if (paras == null) paras = new SqlParameter[0];
            cmd.Parameters.AddRange(paras);
            try
            {
                OpenConnection();
                if (cmd.Transaction == null)
                {
                    cmd.Transaction = con.BeginTransaction();
                }
                int row = cmd.ExecuteNonQuery();
                if (isCommit)
                {
                    cmd.Transaction.Commit();
                    con.Close();
                }
                return row;
            }
            catch (Exception ex)
            {
                cmd.Transaction.Rollback();
                con.Close();
                throw ex;
            }

        }
        #endregion

        #region 对连接执行T-SQL语句并返回受影响的行数，可以使用存储过程
        /// <summary>
        /// 对连接执行T-SQL语句并返回受影响的行数
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="paras">sql参数</param>
        /// <param name="cmdType">命令类型</param>
        /// <returns>受影响的行数</returns>
        public int ExecuteNonQuery(string sql, SqlParameter[] paras, CommandType cmdType)
        {
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.CommandType = cmdType;
            if (paras == null) paras = new SqlParameter[0];
            cmd.Parameters.AddRange(paras);
            try
            {
                OpenConnection();
                int row = cmd.ExecuteNonQuery();
                con.Close();
                return row;
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region 执行查询，并返回查询所返回的结果集中第一行的第一列。忽略其他列或行。
        /// <summary>
        /// 执行查询，并返回查询所返回的结果集中第一行的第一列。忽略其他列或行。
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="paras">sql参数</param>
        /// <returns>结果集中第一行的第一列</returns>
        public object ExecuteScalar(string sql, SqlParameter[] paras)
        {
            try
            {
                return ExecuteScalar(sql, paras, CommandType.Text);
            }
            catch (Exception)
            {
                
                throw;
            }
        }
        #endregion

        #region 执行查询，并返回查询所返回的结果集中第一行的第一列。忽略其他列或行。可以使用存储过程
        /// <summary>
        /// 执行查询，并返回查询所返回的结果集中第一行的第一列。忽略其他列或行。可以使用存储过程
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="paras">sql参数</param>
        /// <param name="cmdType">命令类型</param>
        /// <returns>结果集中第一行的第一列</returns>
        public object ExecuteScalar(string sql, SqlParameter[] paras, CommandType cmdType)
        {
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.CommandType = cmdType;
            if (paras == null) paras = new SqlParameter[0];
            cmd.Parameters.AddRange(paras);
            try
            {
                OpenConnection();
                object result = cmd.ExecuteScalar();
                con.Close();
                return result;
            }
            catch (Exception)
            {
                
                throw;
            }

        }
        #endregion

        #region 执行查询，并返回查询所返回的结果集
        /// <summary>
        /// 执行查询，并返回查询所返回的结果集
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="paras">sql参数</param>
        /// <returns>结果集</returns>
        public SqlDataReader ExecuteReader(string sql, SqlParameter[] paras)
        {
            try
            {
                return ExecuteReader(sql, paras, CommandType.Text);
            }
            catch (Exception)
            {
                
                throw;
            }
        }
        #endregion

        #region 执行查询，并返回查询所返回的结果集。可以使用存储过程
        /// <summary>
        /// 执行查询，并返回查询所返回的结果集。可以使用存储过程
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="paras">sql参数</param>
        /// <param name="cmdType">命令类型</param>
        /// <returns>结果集中第一行的第一列</returns>
        public SqlDataReader ExecuteReader(string sql, SqlParameter[] paras, CommandType cmdType)
        {
            try
            {
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.CommandType = cmdType;
                if (paras == null) paras = new SqlParameter[0];
                cmd.Parameters.AddRange(paras);
                OpenConnection();
                SqlDataReader result = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                return result;
            }
            catch (Exception)
            {
                
                throw;
            }

        }
        #endregion

        #region 根据sql,paras使用dataSet查找数据
        /// <summary>
        /// 根据sql,paras使用dataSet查找数据
        /// </summary>
        /// <param name="sql">执行的sql</param>
        /// <param name="paras">sql的参数</param>
        /// <param name="table">可选参数，数据集的表名，默认是table</param>
        /// <param name="isClear">是否要清空此表的原有数据</param>
        /// <returns>填充了查询数据的数据表</returns>
        public DataTable DataSetFill(string sql, SqlParameter[] paras, string table = "table", bool isClear = false)
        {
            try
            {
                return DataSetFill(sql, paras, CommandType.Text, table, isClear);
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region 根据sql,paras使用dataSet查找数据,可以使用存储过程
        /// <summary>
        /// 根据sql,paras使用dataSet查找数据,可以使用存储过程
        /// </summary>
        /// <param name="sql">执行的sql</param>
        /// <param name="paras">sql的参数</param>
        /// <param name="cmdType">执行命令的类型</param>
        /// <param name="table">可选参数，数据集的表名，默认是table</param>
        /// <param name="isClear">是否要清空此表的原有数据</param>
        /// <returns>填充了查询数据的数据表</returns>
        public DataTable DataSetFill(string sql, SqlParameter[] paras,CommandType cmdType, string table = "table",bool isClear=false)
        {
            if (paras == null) paras = new SqlParameter[0];
            try
            {
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.CommandType = cmdType;
                cmd.Parameters.AddRange(paras);
                SqlCommandBuilder cmdBuilder = new SqlCommandBuilder(dataAdapter);
                dataAdapter.SelectCommand = cmd;
                OpenConnection();
                if (dataSet.Tables.Contains(table) && isClear)
                {
                    dataSet.Tables[table].Clear();
                }
                dataAdapter.Fill(dataSet, table);
                return dataSet.Tables[table];
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion
    }
}
