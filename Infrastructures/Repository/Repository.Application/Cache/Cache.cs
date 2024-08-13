//----------------------------------
//<copyright>FanClass</copyright>
//<author>zhuangjl</author>
//<email>3046524346@qq.com</email>
//<log date="2024-08-11">创建</log>
//---------------------------------

namespace FanClass.Infrastructures.Repository;

/// <summary>
/// 缓存接口实现类
/// </summary>
public class Cache : ICache
{
    #region 构造函数

    /// <summary>
    /// Redis连接Key
    /// </summary>
    private readonly string _connetRedisString;

    /// <summary>
    ///构造函数
    /// </summary>
    /// <param name="configuration"></param>
    /// <param name="redisStoreKey"></param>
    public Cache(IConfiguration configuration, string redisStoreKey = "Cache")
    {
        _connetRedisString = configuration.GetConnectionString(redisStoreKey) ?? string.Empty;
    }

    #endregion 构造函数

    #region 获取Key名

    /// <summary>
    /// 获取Key
    /// </summary>
    /// <param name="type"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    public string GetKey(CacheKeyType type, string value)
    {
        switch (type)
        {
            case CacheKeyType.UserInfo:
                return $"User:Info:Id:{value}";

            default:
                return string.Empty;
        }
    }

    #endregion 获取Key名

    #region 缓存中查询

    /// <summary>
    /// 在缓存中通过Key获取对应值
    /// </summary>
    /// <param name="key"></param>
    /// <param name="defaultDb"></param>
    /// <returns></returns>
    public async Task<(bool hasValue, string value)> TryGetValue(string key, int defaultDb = 0)
    {
        using var connetion = await ConnectionMultiplexer.ConnectAsync(_connetRedisString);
        var db = connetion.GetDatabase(defaultDb);

        var value = await db.StringGetAsync(key);

        return value.HasValue ? (true, value.ToString()) : (false, string.Empty);
    }

    #endregion 缓存中查询

    #region 插入缓存

    /// <summary>
    /// 将指定key与value插入到缓存中
    /// </summary>
    /// <param name="key"></param>
    /// <param name="value"></param>
    /// <param name="expired"></param>
    /// <param name="defaultDb"></param>
    /// <returns></returns>
    public async Task<bool> TryPut(string key, string value, TimeSpan expired, int defaultDb = 0)
    {
        using var connetion = await ConnectionMultiplexer.ConnectAsync(_connetRedisString);
        var db = connetion.GetDatabase(defaultDb);

        return await db.StringSetAsync(key, value, expired);
    }

    #endregion 插入缓存

    #region 缓存中删除

    /// <summary>
    /// 缓存中删除指定Key的元素
    /// </summary>
    /// <param name="key"></param>
    /// <param name="defaultDb"></param>
    /// <returns></returns>
    public async Task<bool> TryDelete(string key, int defaultDb = 0)
    {
        using var connetion = await ConnectionMultiplexer.ConnectAsync(_connetRedisString);
        var db = connetion.GetDatabase(defaultDb);

        return await db.KeyDeleteAsync(key);
    }

    #endregion 缓存中删除
}