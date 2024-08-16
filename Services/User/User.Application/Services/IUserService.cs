//----------------------------------
//<copyright>FanClass</copyright>
//<author>zhuangjl</author>
//<email>3046524346@qq.com</email>
//<log date="2024-08-13">创建</log>
//---------------------------------

namespace FanClass.Services.Users;

/// <summary>
/// 用户业务逻辑层
/// </summary>
public interface IUserService
{
    #region 获取用户

    /// <summary>
    /// 根据主键Id获取用户
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<User?> GetUser(long id);

    #endregion 获取用户
}