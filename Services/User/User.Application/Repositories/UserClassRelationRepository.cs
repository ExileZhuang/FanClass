//----------------------------------
//<copyright>FanClass</copyright>
//<author>zhuangjl</author>
//<email>3046524346@qq.com</email>
//<log date="2024-08-15">创建</log>
//---------------------------------

namespace FanClass.Services.Users;

/// <summary>
/// 学生课堂关系接口实现类
/// </summary>
public class UserClassRelationRepository : Repository<UserClassRelation>, IUserClassRelationRepository
{
    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="configuration"></param>
    /// <param name="tableName"></param>
    /// <param name="dbStoreKey"></param>
    public UserClassRelationRepository(IConfiguration configuration, string tableName = "fc_users_classes", string dbStoreKey = "MySql")
        : base(configuration, tableName, dbStoreKey)
    {
    }
}