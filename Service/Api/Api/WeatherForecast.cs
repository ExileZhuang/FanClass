///----------------------------------
///<copyright>FanClass</copyright>
///<author>zhuangjl</author>
///<email>3046524346@qq.com</email>
///<log date="2024-08-05">����</log>
///---------------------------------

namespace Api
{
    /// <summary>
    /// ����Ԥ��
    /// </summary>
    public class WeatherForecast
    {
        /// <summary>
        /// ʱ��
        /// </summary>
        public DateOnly Date { get; set; }

        /// <summary>
        /// �¶�
        /// </summary>
        public int TemperatureC { get; set; }

        /// <summary>
        /// �¶�
        /// </summary>
        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

        /// <summary>
        /// ��Ҫ
        /// </summary>
        public string? Summary { get; set; }
    }
}