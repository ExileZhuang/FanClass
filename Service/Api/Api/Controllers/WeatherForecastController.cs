///----------------------------------
///<copyright>FanClass</copyright>
///<author>zhuangjl</author>
///<email>3046524346@qq.com</email>
///<log date="2024-08-05">创建</log>
///---------------------------------

namespace Api.Controllers
{
    /// <summary>
    /// 天气预测接口
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        /// <summary>
        /// 初始化私有变量
        /// </summary>
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        /// <summary>
        /// 日志
        /// </summary>
        private readonly ILogger<WeatherForecastController> _logger;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="logger"></param>
        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// 获取天气预测
        /// </summary>
        /// <returns></returns>
        [HttpGet(Name = "WeatherForecast")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<WeatherForecast>))]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}