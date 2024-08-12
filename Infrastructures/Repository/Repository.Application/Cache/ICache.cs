//----------------------------------
//<copyright>FanClass</copyright>
//<author>zhuangjl</author>
//<email>3046524346@qq.com</email>
//<log date="2024-08-11">创建</log>
//---------------------------------

namespace FanClass.Infrastructures.Repository;

/// <summary>
/// 缓存接口
/// </summary>
public interface ICache
{
    #region 获取Key名

    /// <summary>
    /// 获取在缓存中指定类型Key名
    /// </summary>
    /// <param name="type"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    string GetKey(CacheKeyType type, string value);

    #endregion 获取Key名

    #region 缓存中查询

    /// <summary>
    /// 缓存中查询
    /// </summary>
    /// <param name="key"></param>
    /// <param name="defaultDb"></param>
    /// <returns></returns>
    Task<(bool hasValue, string value)> TryGetValue(string key, int defaultDb = 0);

    #endregion 缓存中查询

    #region 插入缓存

    /// <summary>
    /// 插入缓存
    /// </summary>
    /// <param name="key"></param>
    /// <param name="value"></param>
    /// <param name="time"></param>
    /// <returns></returns>
    Task<bool> TryPut(string key, string value, TimeSpan time);

    #endregion 插入缓存

    #region 缓存中删除

    /// <summary>
    /// 删除缓存中指定Key的对应值
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    Task<int> TryDelete(string key);

    #endregion 缓存中删除
}