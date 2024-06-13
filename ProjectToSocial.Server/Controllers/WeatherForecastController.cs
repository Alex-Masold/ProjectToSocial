using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectToSocial.Server.DataContext;

namespace ProjectToSocial.Server.Controllers
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

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetWeatherForecast")]
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
    

        //[HttpPost("Authentication")]
        //public async Task<ActionResult<int>> Authentication([FromBody] AuthenticationRequest request)
        //{
        //    using (ApplicationContext db = new ApplicationContext())
        //    {
        //        var User = await db.Users
        //            .FirstOrDefaultAsync(_user => _user.Email == request.Email && _user.Password == request.Password);
        //        if (User == null)
        //        {
        //            return NotFound("Invalid email or password");
        //        }

        //        return Ok(User.Id);
        //    }
        //}
        //public class AuthenticationRequest
        //{
        //    public string Email { get; set; }
        //    public string Password { get; set; }
        //}
    }
}
