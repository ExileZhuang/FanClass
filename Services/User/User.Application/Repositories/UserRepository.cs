//----------------------------------
//<copyright>FanClass</copyright>
//<author>zhuangjl</author>
//<email>3046524346@qq.com</email>
//<log date="2024-08-13">创建</log>
//---------------------------------

using Microsoft.Extensions.Configuration;

namespace FanClass.Services.User;

/// <summary>
/// 用户仓储接口实现类
/// </summary>
public class UserRepository : Repository<User>, IUserRepository
{
    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="configuration"></param>
    /// <param name="tableName"></param>
    /// <param name="dbStoreKey"></param>
    /// <param name="redisStoreKey"></param>
    public UserRepository(IConfiguration configuration, string tableName = "fc_users", string dbStoreKey = "MySql", string redisStoreKey = "Cache")
        : base(configuration, tableName, dbStoreKey, redisStoreKey)
    {
    }
}