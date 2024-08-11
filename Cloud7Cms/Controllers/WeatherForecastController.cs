using Cloud7Cms.Configuration;
using Cloud7Cms.DapperConfig;
using Microsoft.AspNetCore.Mvc;

namespace Cloud7Cms.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
      
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly PaymentGrameenphoneDapperContext _grameenphoneDapperContext;
         
        public WeatherForecastController(ILogger<WeatherForecastController> logger
            , PaymentGrameenphoneDapperContext grameenphoneDapperContext)
        {
            _logger = logger;
            _grameenphoneDapperContext = grameenphoneDapperContext;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IActionResult Get()
        {
            var serviceMaps = _grameenphoneDapperContext.Query<WeatherForecast>("SELECT NewServiceId as Summary FROM ServiceMaps");
            return Ok(serviceMaps);
        }
    }
}
