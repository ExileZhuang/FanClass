//----------------------------------
//<copyright>FanClass</copyright>
//<author>zhuangjl</author>
//<email>3046524346@qq.com</email>
//<log date="2024-08-14">创建</log>
//---------------------------------

namespace FanClass.Infrastructures.Tools;

/// <summary>
/// 返回结果
/// </summary>
public class Result
{
    /// <summary>
    /// 状态
    /// </summary>
    public string State { get; set; } = string.Empty;

    /// <summary>
    /// 数据
    /// </summary>
    public object? Data { get; set; } = null;

    /// <summary>
    /// 成功
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    public static Result Success(object? data)
    {
        return new Result
        {
            State = "200",
            Data = data
        };
    }

    /// <summary>
    /// 失败
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    public static Result Error(object? data)
    {
        return new Result
        {
            State = "400",
            Data = data
        };
    }

    /// <summary>
    /// 未找到
    /// </summary>
    /// <returns></returns>
    public static Result NotFound()
    {
        return new Result
        {
            State = "300",
            Data = null
        };
    }
}