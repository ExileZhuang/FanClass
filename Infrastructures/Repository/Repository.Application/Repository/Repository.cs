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
    /// <param name="type"></param>
    /// <returns></returns>
    public async Task<T> TryCacheGet(long id, CacheKeyType type)
    {
        if (_cacheUsed)
        {
            var key = _cache!.GetKey(type, id.ToString());

            var (hasValue, value) = await _cache.TryGetValue(key);

            if (hasValue)
            {
                var res = JsonConvert.DeserializeObject<T>(value);

                if (res is not null)
                {
                    return (T)res;
                }
            }
        }

        return await Get(id);
    }

    /// <summary>
    /// 主键在数据库中获取
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<T> Get(long id)
    {
        using var dbConnection = new MySqlConnection(_connetMySqlString);
        dbConnection.Open();

        var sql = $"SELECT * FROM {_tableName} WHERE Id=@Id;";
        var queryResult = await dbConnection.QueryAsync<T>(sql, new { Id = id });

        return queryResult.First();
    }

    /// <summary>
    /// 通过指定参数筛选数据
    /// </summary>
    /// <param name="parameters"></param>
    /// <returns></returns>
    public async Task<IEnumerable<T>> Gets(Dictionary<string, object> parameters)
    {
        using var dbConnection = new MySqlConnection(_connetMySqlString);
        dbConnection.Open();

        var paramString = new string[parameters.Count];
        var index = 0;

        foreach (var keyValue in parameters)
        {
            paramString[index++] = $"{keyValue.Key}=@{keyValue}";
        }

        var sql = $"SELECT * FROM {_tableName} WHERE {string.Join(" and ", paramString)};";

        return await dbConnection.QueryAsync<T>(sql, parameters);
    }

    #endregion 查询

    #region 插入

    /// <summary>
    /// 插入单个实体
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    public async Task<bool> Insert(T entity)
    {
        using var dbConnection = new MySqlConnection(_connetMySqlString);
        dbConnection.Open();

        var names = new List<string>();
        var values = new List<string>();

        foreach (var pInfo in entity.GetType().GetProperties())
        {
            names.Add(pInfo.Name);

            var value = pInfo!.GetValue(entity)!.ToString() ?? string.Empty;
            if (pInfo.PropertyType == typeof(string))
            {
                value = "'" + value + "'";
            }

            values.Add(value);
        }

        var sql = $"INSERT INTO {_tableName} ({string.Join(", ", names)}) VALUES ({string.Join(",", values)});";

        var result = await dbConnection.ExecuteAsync(sql);

        return result > 0;
    }

    /// <summary>
    /// 将数据T插入到数据库中，并写入缓存，过期时间为expired
    /// </summary>
    /// <param name="entity"></param>
    /// <param name="expired"></param>
    /// <param name="type"></param>
    /// <returns></returns>
    public async Task<bool> TryCacheInsert(T entity, TimeSpan expired, CacheKeyType type)
    {
        if (!_cacheUsed)
        {
            return false;
        }

        string cacheKey = _cache!.GetKey(type, (entity!.GetType())?.GetProperty("Id")!.ToString() ?? string.Empty);

        string value = JsonConvert.SerializeObject(entity);

        bool putSucc = await _cache.TryPut(cacheKey, value, expired);

        if (!putSucc)
        {
            return false;
        }

        putSucc = await Insert(entity);

        return putSucc;
    }

    #endregion 插入

    #region 删除

    /// <summary>
    /// 删除对应主键的唯一实体
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<int> Delete(long id)
    {
        using var db = new MySqlConnection(_connetMySqlString);
        db.Open();

        var sql = $"DELETE FROM {_tableName} WHERE Id=@Id;";

        return await db.ExecuteAsync(sql, new { Id = id });
    }

    /// <summary>
    /// 删除复合参数的所有实体
    /// </summary>
    /// <param name="parameters"></param>
    /// <returns></returns>
    public async Task<int> Deletes(Dictionary<string, object> parameters)
    {
        using var db = new MySqlConnection(_connetMySqlString);
        db.Open();

        var selections = new string[parameters.Count];

        int index = 0;

        foreach (var keyValue in parameters)
        {
            var selection = string.Empty;
            if (keyValue.Value is string)
            {
                selection = $"{keyValue.Key}='{keyValue.Value}'";
            }
            else
            {
                selection = selection + $"{keyValue.Value}";
            }

            selections[index++] = selection;
        }

        var sql = $"DELETE FROM {_tableName} WHERE {string.Join(" and ", selections)};";

        return await db.ExecuteAsync(sql);
    }

    #endregion 删除

    #region 更新

    /// <summary>
    /// 更新单个实体
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    public async Task<int> Update(T entity)
    {
        using var db = new MySqlConnection(_connetMySqlString);
        db.Open();

        var updates = new List<string>();

        foreach (var pInfo in entity.GetType().GetProperties())
        {
            var value = pInfo!.GetValue(entity)!.ToString() ?? string.Empty;

            string item;

            if (pInfo.PropertyType == typeof(string))
            {
                item = $"{pInfo.Name}='{value}'";
            }
            else
            {
                item = $"{pInfo.Name}={value}";
            }

            updates.Add(item);
        }

        var sql = $"UPDATE {_tableName} SET {string.Join(',', updates)} WHERE Id=@Id;";

        var info = entity.GetType().GetProperty("Id");
        var id = info?.GetValue(obj: entity);

        return await db.ExecuteAsync(sql, new { Id = id });
    }

    /// <summary>
    /// 批量更新多个实体
    /// </summary>
    /// <param name="selections"></param>
    /// <param name="updates"></param>
    /// <returns></returns>
    public async Task<int> Updates(Dictionary<string, object> selections, Dictionary<string, object> updates)
    {
        using var db = new MySqlConnection(_connetMySqlString);
        db.Open();

        var listUpdates = new List<string>();

        foreach (var item in updates)
        {
            var update = string.Empty;

            if (item.Value.GetType() == typeof(string))
            {
                update = $"{item.Key}='{item.Value}'";
            }
            else
            {
                update = $"{item.Key}={item.Value}";
            }

            listUpdates.Add(update);
        }

        var listSelections = new List<string>();

        foreach (var item in selections)
        {
            var selection = string.Empty;

            if (item.Value.GetType() == typeof(string))
            {
                selection = $"{item.Key}='{item.Value}'";
            }
            else
            {
                selection = $"{item.Key}={item.Value}"; ;
            }

            listSelections.Add(selection);
        }

        var sql = $"UPDATE {_tableName} SET {string.Join(',', listUpdates)} WHERE {string.Join(" and ", listSelections)};";

        return await db.ExecuteAsync(sql);
    }

    #endregion 更新
}