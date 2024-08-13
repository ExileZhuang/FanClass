//----------------------------------
//<copyright>FanClass</copyright>
//<author>zhuangjl</author>
//<email>3046524346@qq.com</email>
//<log date="2024-08-07">创建</log>
//---------------------------------

namespace FanClass.Services.User;

/// <summary>
/// 用户模型
/// </summary>
public class User
{
    #region 基本属性

    /// <summary>
    /// 主键Id
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// 密码（已加密）
    /// </summary>
    public string Password { get; set; } = string.Empty;

    /// <summary>
    /// 学号/证件号
    /// </summary>
    public string Number { get; set; } = string.Empty;

    /// <summary>
    /// 姓名
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// 电话号码
    /// </summary>
    public string Mobile { get; set; } = string.Empty;

    /// <summary>
    /// 身份
    /// </summary>
    public UserIdentity Identity { get; set; }

    /// <summary>
    /// 创建日期
    /// </summary>
    public DateTime DateCreated { get; set; }

    /// <summary>
    /// 上一次修改日期
    /// </summary>
    public DateTime DateModified { get; set; }

    #endregion 基本属性
}