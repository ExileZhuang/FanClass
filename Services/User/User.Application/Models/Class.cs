//----------------------------------
//<copyright>FanClass</copyright>
//<author>zhuangjl</author>
//<email>3046524346@qq.com</email>
//<log date="2024-08-14">创建</log>
//---------------------------------

namespace FanClass.Services.Users;

/// <summary>
/// 课堂模型
/// </summary>
public class Class
{
    #region 常规属性

    /// <summary>
    /// 主键Id
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// 课程名字
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// 列大小
    /// </summary>
    public int ColSize { get; set; }

    /// <summary>
    /// 行大小
    /// </summary>
    public int RowSize { get; set; }

    /// <summary>
    /// 课序号
    /// </summary>
    public string Number { get; set; } = string.Empty;

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime DateCreated { get; set; }

    /// <summary>
    /// 上一次修改时间
    /// </summary>
    public DateTime DateModified { get; set; }

    #endregion 常规属性
}