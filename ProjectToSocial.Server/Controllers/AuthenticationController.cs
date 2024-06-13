using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectToSocial.Server.DataContext;

namespace ProjectToSocial.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly ILogger<AuthenticationController> _logger;

        public AuthenticationController(ILogger<AuthenticationController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public async Task<ActionResult<int>> Authentication([FromBody] AuthenticationRequest request)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var user = await db.Users
                    .Include(_user => _user.Role)
                    .FirstOrDefaultAsync(_user => _user.Email == request.Email && _user.Password == request.Password);
                if (user == null)
                {
                    return NotFound("Invalid email or password");
                }

                return Ok(new AuthenticationAnswer() { Id = user.Id, Role = user.Role.Name});
            }
        }
        public class AuthenticationRequest
        {
            public string Email { get; set; }
            public string Password { get; set; }
        }
        public class AuthenticationAnswer
        {
            public int Id { get; set; }
            public string Role { get; set; }
        }
    }
}
