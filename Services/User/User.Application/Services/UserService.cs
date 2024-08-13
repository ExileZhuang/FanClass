//----------------------------------
//<copyright>FanClass</copyright>
//<author>zhuangjl</author>
//<email>3046524346@qq.com</email>
//<log date="2024-08-13">创建</log>
//---------------------------------

namespace FanClass.Services.User;

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
}