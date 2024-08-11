//----------------------------------
//<copyright>FanClass</copyright>
//<author>zhuangjl</author>
//<email>3046524346@qq.com</email>
//<log date="2024-08-10">创建</log>
//---------------------------------

namespace FanClass.Infrastructures.Repository;

/// <summary>
/// 仓储接口基类
/// </summary>
/// <typeparam name="T"></typeparam>
public class Repository<T> : IRepository<T> where T : class
{
    #region 构造函数

    /// <summary>
    /// 表名
    /// </summary>
    protected readonly string _tableName;

    /// <summary>
    /// 连接MySql字符串
    /// </summary>
    protected readonly string _connetMySqlString;

    /// <summary>
    /// 是否使用RedisCache
    /// </summary>
    protected readonly bool _cacheUsed;

    /// <summary>
    ///连接Redis字符串
    /// </summary>
    protected readonly string _connetRedisString;

    /// <summary>
    /// 缓存
    /// </summary>
    protected readonly ICache? _cache;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="configuration"></param>
    /// <param name="tableName"></param>
    /// <param name="dbStoreKey"></param>
    public Repository(IConfiguration configuration, string tableName, string dbStoreKey = "MySql")
    {
        _tableName = tableName;
        _connetMySqlString = configuration.GetConnectionString(dbStoreKey) ?? string.Empty;
        _cacheUsed = false;
        _connetRedisString = string.Empty;
        _cache = null;
    }

    /// <summary>
    /// 具有缓存的构造函数
    /// </summary>
    /// <param name="configuration"></param>
    /// <param name="tableName"></param>
    /// <param name="dbStoreKey"></param>
    /// <param name="redisStoreKey"></param>
    public Repository(IConfiguration configuration, string tableName, string dbStoreKey = "MySql", string redisStoreKey = "Cache")
    {
        _tableName = tableName;
        _connetMySqlString = configuration.GetConnectionString(dbStoreKey) ?? string.Empty;
        _cacheUsed = true;
        _connetRedisString = configuration.GetConnectionString(redisStoreKey) ?? string.Empty;
        _cache = new Cache(configuration, _connetRedisString);
    }

    #endregion 构造函数

    #region 查询

    /// <summary>
    /// 尝试在Redis中通过主键获取，如果Redis中没有则去数据库中查询主键获取
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<T> TryCacheGet(long id)
    {
        if (_cacheUsed)
        {
            var key = _cache!.GetKey(CacheKeyType.UserInfo, id.ToString());
            var (hasValue, value) = await _cache.TryGetValue(key);
        }

        using var dbConnection = new MySqlConnection(_connetMySqlString);
        dbConnection.Open();

        var sql = $"SELECT * FROM {_tableName} WHERE Id=@Id";
        var queryResult = await dbConnection.QueryAsync<T>(sql, new { Id = id });

        return queryResult.First();
    }

    #endregion 查询
}