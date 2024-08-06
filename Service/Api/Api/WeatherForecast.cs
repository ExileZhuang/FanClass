///----------------------------------
///<copyright>FanClass</copyright>
///<author>zhuangjl</author>
///<email>3046524346@qq.com</email>
///<log date="2024-08-05">创建</log>
///---------------------------------

namespace Api
{
    /// <summary>
    /// 天气预测
    /// </summary>
    public class WeatherForecast
    {
        /// <summary>
        /// 时间
        /// </summary>
        public DateOnly Date { get; set; }

        /// <summary>
        /// 温度
        /// </summary>
        public int TemperatureC { get; set; }

        /// <summary>
        /// 温度
        /// </summary>
        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

        /// <summary>
        /// 概要
        /// </summary>
        public string? Summary { get; set; }
    }
}