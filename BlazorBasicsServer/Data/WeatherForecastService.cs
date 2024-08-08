namespace BlazorBasicsServer.Data
{
    public class WeatherForecastService
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly IDataDemo _db;
        private readonly ILogger<WeatherForecastService> _logger;
        //Get a configuration value
        private readonly IConfiguration _config;
        public WeatherForecastService(IConfiguration config, IDataDemo db, ILogger<WeatherForecastService> logger)
        {
            _db = db;
            _logger = logger;
            _config = config;
        }

        public Task<WeatherForecast[]> GetForecastAsync(DateOnly startDate)
        {
            //Dependency injection
            var rndNum = _db.GetAge();

            //Trying logger
            _logger.LogInformation($"Random number from BD: {rndNum}");


            //Get a value from settings (Priority: secrets > appsettings.<environment> > appsettings)
            var valMax = _config.GetValue<int>("WeatherForeCast:ForeCastDays"); //It depends on the json structure


            return Task.FromResult(Enumerable.Range(1, valMax).Select(index => new WeatherForecast
            {
                Date = startDate.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            }).ToArray());
        }
    }
}
