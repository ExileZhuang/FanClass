//----------------------------------
//<copyright>FanClass</copyright>
//<author>zhuangjl</author>
//<email>3046524346@qq.com</email>
//<log date="2024-08-14">创建</log>
//---------------------------------

namespace FanClass.Services.User;

/// <summary>
/// 课堂仓储接口实现类
/// </summary>
public class ClassRepository : Repository<Class>, IRepository<Class>
{
    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="configuration"></param>
    /// <param name="tableName"></param>
    /// <param name="dbStoreKey"></param>
    public ClassRepository(IConfiguration configuration, string tableName = "fc_classes", string dbStoreKey = "MySql")
        : base(configuration, tableName, dbStoreKey)
    {
    }
}