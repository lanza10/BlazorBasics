namespace BlazorBasicsServer.Data
{
    public class WeatherForecastService
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly IDataDemo _db;

        public WeatherForecastService(IDataDemo db)
        {
            _db = db;
        }

        public Task<WeatherForecast[]> GetForecastAsync(DateOnly startDate)
        {
            //Dependency injection
            var maxNum = _db.GetAge();

            return Task.FromResult(Enumerable.Range(1, maxNum).Select(index => new WeatherForecast
            {
                Date = startDate.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            }).ToArray());
        }
    }
}
