//----------------------------------
//<copyright>FanClass</copyright>
//<author>zhuangjl</author>
//<email>3046524346@qq.com</email>
//<log date="2024-08-14">创建</log>
//---------------------------------

namespace FanClass.Services.Users;

/// <summary>
/// 用户课堂关系：多对多
/// </summary>
public class UserClassRelation
{
    #region 常规属性

    /// <summary>
    /// 主键Id
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// 用户Id
    /// </summary>
    public long UserId { get; set; }

    /// <summary>
    /// 课堂Id
    /// </summary>
    public long ClassId { get; set; }

    /// <summary>
    /// 该用户所选列
    /// </summary>
    public int ColIndex { get; set; }

    /// <summary>
    /// 用户所选行
    /// </summary>
    public int RowIndex { get; set; }

    /// <summary>
    /// 该记录是否被删除
    /// </summary>
    public DateTime SelectedTime { get; set; }

    #endregion 常规属性
}