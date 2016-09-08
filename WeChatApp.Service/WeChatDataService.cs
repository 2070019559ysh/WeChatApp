using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using WeChatApp.Model;
using WetChatApp.Core.IService;

namespace WeChatApp.Service
{
    /// <summary>
    /// 微信对接涉及的数据进行保存、查询等操作的实现类
    /// </summary>
    public class WeChatDataService:IWeChatDataService
    {
        private SqlHelper sqlHelper =new SqlHelper();

        /// <summary>
        /// 新增一条微信数据记录
        /// </summary>
        /// <param name="weChatData">微信数据对象</param>
        /// <returns>是否新增成功</returns>
        public bool InsertWeChatData(WeChatData weChatData)
        {
            string sql="INSERT INTO WeChatData([Key],Value,UpdateTime) VALUES(@key,@value,@updateTime)";
            SqlParameter[] paras=new SqlParameter[]{
            new SqlParameter("@key",weChatData.Key),
            new SqlParameter("@value",weChatData.Value),
            new SqlParameter("@updateTime",weChatData.UpdateTime)
            };
            int row = sqlHelper.ExecuteNonQuery(sql, paras);
            return row > 0;
        }

        /// <summary>
        /// 根据微信数据Id查找微信数据对象
        /// </summary>
        /// <param name="id">微信数据Id</param>
        /// <returns>微信数据对象</returns>
        public WeChatData SearchWeChatData(int id)
        {
            string sql = "SELECT Id,[Key],Value,UpdateTime FROM WeChatData WHERE Id=@id";
            SqlParameter param = new SqlParameter("@id", id);
            SqlDataReader dataReader=sqlHelper.ExecuteReader(sql, new SqlParameter[]{param});
            WeChatData weChatData = null;
            if (dataReader.NextResult())
            {
                weChatData = new WeChatData()
                {
                    Id = id,
                    Key = dataReader["Key"].ToString(),
                    Value = dataReader["Value"].ToString(),
                    UpdateTime = Convert.ToDateTime(dataReader["UpdateTime"])
                };
            }
            return weChatData;
        }

        /// <summary>
        /// 根据微信数据的Key查找微信数据对象
        /// </summary>
        /// <param name="id">微信数据的Key</param>
        /// <returns>微信数据对象</returns>
        public WeChatData SearchWeChatData(string key)
        {
            string sql = "SELECT Id,[Key],Value,UpdateTime FROM WeChatData WHERE Key=@key";
            SqlParameter param = new SqlParameter("@key", key);
            SqlDataReader dataReader = sqlHelper.ExecuteReader(sql, new SqlParameter[] { param });
            WeChatData weChatData = null;
            if (dataReader.NextResult())
            {
                weChatData = new WeChatData()
                {
                    Id = Convert.ToInt32(dataReader["Id"]),
                    Key = dataReader["Key"].ToString(),
                    Value = dataReader["Value"].ToString(),
                    UpdateTime = Convert.ToDateTime(dataReader["UpdateTime"])
                };
            }
            return weChatData;
        }
    }
}
