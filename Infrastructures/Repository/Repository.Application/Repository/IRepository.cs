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
    /// 尝试在Redis中通过主键获取，如果Redis中没有则去数据库中查询主键获取
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<T> TryCacheGet(long id);

    #endregion 查询
}