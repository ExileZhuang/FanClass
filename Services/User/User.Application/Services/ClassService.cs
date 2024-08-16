//----------------------------------
//<copyright>FanClass</copyright>
//<author>zhuangjl</author>
//<email>3046524346@qq.com</email>
//<log date="2024-08-15">创建</log>
//---------------------------------

namespace FanClass.Services.Users;

/// <summary>
/// 课堂业务逻辑实现类
/// </summary>
public class ClassService : IClassService
{
    /// <summary>
    /// 课堂仓储接口
    /// </summary>
    private readonly IClassRepository _classRepository;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="classRepository"></param>
    public ClassService(IClassRepository classRepository)
    {
        _classRepository = classRepository;
    }
}