//----------------------------------
//<copyright>FanClass</copyright>
//<author>zhuangjl</author>
//<email>3046524346@qq.com</email>
//<log date="2024-08-13">创建</log>
//---------------------------------

/// <summary>
/// 用户业务逻辑实现层
/// </summary>
public class UserService : IUserService
{
    /// <summary>
    /// 用户仓储接口
    /// </summary>
    private readonly IUserRepository _userRepository;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="userRepository"></param>
    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    #region 获取用户

    /// <summary>
    /// 根据主键Id获取用户
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<User?> GetUser(long id)
    {
        return await _userRepository.Get(id);
    }

    #endregion 获取用户
}