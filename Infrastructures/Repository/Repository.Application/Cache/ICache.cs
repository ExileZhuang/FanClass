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
}