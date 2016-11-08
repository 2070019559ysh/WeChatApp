using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using WeChatApp.Model;
using WeChatApp.Core.IService;
using WetChatApp.Common;

namespace WeChatApp.Service
{
    /// <summary>
    /// 微信对接涉及的数据进行保存、查询等操作的实现类
    /// </summary>
    public class WeChatDataService:IWeChatDataService
    {
        private SqlHelper sqlHelper =new SqlHelper();
        private readonly LogHelper log = new LogHelper(typeof(WeChatDataService));

        /// <summary>
        /// 新增一条微信数据记录
        /// </summary>
        /// <param name="weChatData">微信数据对象</param>
        /// <returns>是否新增成功</returns>
        public bool InsertWeChatData(WeChatData weChatData)
        {
            log.InfoFormat("新增一条微信数据记录InsertWeChatData((weChatData.Key:{0},weChatData.Value:{1})", weChatData.Key, weChatData.Value);
            string sql="INSERT INTO WeChatData([Key],Value,UpdateTime) VALUES(@key,@value,@updateTime)";
            SqlParameter[] paras=new SqlParameter[]{
            new SqlParameter("@key",weChatData.Key),
            new SqlParameter("@value",weChatData.Value),
            new SqlParameter("@updateTime",weChatData.UpdateTime)
            };
            int row = sqlHelper.ExecuteNonQuery(sql, paras);
            log.InfoFormat("新增一条微信数据记录InsertWeChatData(),影响行数：", row);
            return row > 0;
        }

        /// <summary>
        /// 添加或更新微信数据记录，不存在同Key则添加，存在则更新
        /// </summary>
        /// <param name="weChatData">微信数据对象</param>
        /// <returns>是否新增或更新成功</returns>
        public bool InsertOrUpdateWeChatData(WeChatData weChatData)
        {
            log.InfoFormat("添加或更新微信数据记录InsertOrUpdateWeChatData(weChatData.Key:{0},weChatData.Value:{1})", weChatData.Key, weChatData.Value);
            WeChatData wechatData = SearchWeChatData(weChatData.Key);
            if (weChatData == null)
            {
                log.InfoFormat("添加或更新微信数据记录InsertOrUpdateWeChatData(),不存在Key:{0}，添加记录", weChatData.Key);
                return InsertWeChatData(weChatData);
            }
            else
            {
                log.InfoFormat("添加或更新微信数据记录InsertOrUpdateWeChatData(),存在Key:{0}，更新记录", weChatData.Key);
                return UpdateWeChatData(weChatData);
            }
        }

        /// <summary>
        /// 根据Id或Key更新微信数据记录
        /// </summary>
        /// <param name="weChatData">微信数据对象</param>
        /// <returns>是否更新成功</returns>
        public bool UpdateWeChatData(WeChatData weChatData)
        {
            log.Info("根据Id或Key更新微信数据记录，UpdateWeChatData()");
            string sql = "UPDATE WeChatData SET Value=@value,UpdateTime=@updateTime WHERE Id=@id OR [Key]=@key";
            SqlParameter[] param = new SqlParameter[]{
                new SqlParameter("@value",weChatData.Value),
                new SqlParameter("@updateTime",weChatData.UpdateTime),
                new SqlParameter("@id",weChatData.Id),
                new SqlParameter("@key",weChatData.Key)
            };
            bool result = sqlHelper.ExecuteNonQuery(sql, param) > 0;
            log.Info("根据Id或Key更新微信数据记录，UpdateWeChatData()，返回：" + result);
            return result;
        }

        /// <summary>
        /// 根据微信数据Id查找微信数据对象
        /// </summary>
        /// <param name="id">微信数据Id</param>
        /// <returns>微信数据对象</returns>
        public WeChatData SearchWeChatData(int id)
        {
            log.InfoFormat("根据微信数据Id查找微信数据对象_SearchWeChatData(id:{0})", id);
            string sql = "SELECT Id,[Key],Value,UpdateTime FROM WeChatData WHERE Id=@id";
            SqlParameter param = new SqlParameter("@id", id);
            SqlDataReader dataReader = null;
            WeChatData weChatData = null;
            try
            {
                dataReader = sqlHelper.ExecuteReader(sql, new SqlParameter[] { param });
                if (dataReader.Read())
                {
                    log.Info("根据微信数据Id查找微信数据对象_SearchWeChatData()，读取到一条记录");
                    weChatData = new WeChatData()
                    {
                        Id = id,
                        Key = dataReader["Key"].ToString(),
                        Value = dataReader["Value"].ToString(),
                        UpdateTime = Convert.ToDateTime(dataReader["UpdateTime"])
                    };
                }
            }
            finally
            {
                if (dataReader != null)
                    dataReader.Close();
            }
            log.InfoFormat("根据微信数据Id查找微信数据对象_SearchWeChatData()，返回：weChatData.Value={0}", weChatData != null ? weChatData.Value : "null");
            return weChatData;
        }

        /// <summary>
        /// 根据微信数据的Key查找微信数据对象
        /// </summary>
        /// <param name="id">微信数据的Key</param>
        /// <returns>微信数据对象</returns>
        public WeChatData SearchWeChatData(string key)
        {
            log.InfoFormat("根据微信数据的Key查找微信数据对象_SearchWeChatData(key:{0})", key);
            string sql = "SELECT Id,[Key],Value,UpdateTime FROM WeChatData WHERE [Key]=@key";
            SqlParameter param = new SqlParameter("@key", key);
            SqlDataReader dataReader = sqlHelper.ExecuteReader(sql, new SqlParameter[] { param });
            WeChatData weChatData = null;
            if (dataReader.Read())
            {
                log.Info("根据微信数据的Key查找微信数据对象_SearchWeChatData()，读取到一条数据");
                weChatData = new WeChatData()
                {
                    Id = Convert.ToInt32(dataReader["Id"]),
                    Key = dataReader["Key"].ToString(),
                    Value = dataReader["Value"].ToString(),
                    UpdateTime = Convert.ToDateTime(dataReader["UpdateTime"])
                };
            }
            dataReader.Close();
            log.InfoFormat("根据微信数据的Key查找微信数据对象_SearchWeChatData()，返回：weChatData.Id={0}", (weChatData != null ? weChatData.Id.ToString() : "null"));
            return weChatData;
        }
    }
}
