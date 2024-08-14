//----------------------------------
//<copyright>FanClass</copyright>
//<author>zhuangjl</author>
//<email>3046524346@qq.com</email>
//<log date="2024-08-10">创建</log>
//---------------------------------

namespace FanClass.Infrastructures.Repository;

/// <summary>
/// 仓储接口
/// </summary>
public interface IRepository<T>
{
    #region 查询

    /// <summary>
    /// 尝试在Redis中通过主键获取，如果Redis中没有则去MySql中查询主键获取
    /// </summary>
    /// <param name="id"></param>
    /// <param name="type"></param>
    /// <returns></returns>
    Task<T?> TryCacheGet(long id, CacheKeyType type);

    /// <summary>
    /// 在数据库中获取
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<T?> Get(long id);

    /// <summary>
    /// 根据指定参数筛选MySql中数据
    /// </summary>
    /// <param name="parameters"></param>
    /// <returns></returns>
    Task<IEnumerable<T>?> Gets(Dictionary<string, object> parameters);

    #endregion 查询

    #region 插入

    /// <summary>
    /// 插入到数据库中
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    Task<bool> Insert(T entity);

    /// <summary>
    /// 将数据T插入到数据库中，并写入缓存，过期时间为expired
    /// </summary>
    /// <param name="entity"></param>
    /// <param name="expired"></param>
    /// <param name="type"></param>
    /// <returns></returns>
    Task<bool> TryCacheInsert(T entity, TimeSpan expired, CacheKeyType type);

    #endregion 插入

    #region 删除

    /// <summary>
    /// 删除指定主键的单个实体
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<int> Delete(long id);

    /// <summary>
    /// 删除满足parameters的所有实体
    /// </summary>
    /// <param name="parameters"></param>
    /// <returns></returns>
    Task<int> Deletes(Dictionary<string, object> parameters);

    #endregion 删除

    #region 更新

    /// <summary>
    /// 更新单个实体
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    Task<int> Update(T entity);

    /// <summary>
    /// 批量更新多个实体
    /// </summary>
    /// <param name="selections"></param>
    /// <param name="updates"></param>
    /// <returns></returns>
    Task<int> Updates(Dictionary<string, object> selections, Dictionary<string, object> updates);

    #endregion 更新
}