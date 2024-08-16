//----------------------------------
//<copyright>FanClass</copyright>
//<author>zhuangjl</author>
//<email>3046524346@qq.com</email>
//<log date="2024-08-15">创建</log>
//---------------------------------

namespace FanClass.Services.Api;

/// <summary>
/// 用户API控制层
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    /// <summary>
    /// 用户业务逻辑接口
    /// </summary>
    private readonly IUserService _userServce;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="userServce"></param>
    public UserController(IUserService userServce)
    {
        _userServce = userServce;
    }

    /// <summary>
    /// 通过Id获取用户信息
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("User")]
    [ProducesResponseType(200, Type = typeof(User))]
    public async Task<Result> GetUser(long id)
    {
        User? user = await _userServce.GetUser(id);

        if (user == null)
        {
            return Result.NotFound();
        }

        return Result.Success(user);
    }
}