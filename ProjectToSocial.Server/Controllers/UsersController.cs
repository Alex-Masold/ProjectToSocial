using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using ProjectToSocial.Server.DataContext;
using ProjectToSocial.Server.Models;

namespace ProjectToSocial.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<ICollection<User>>> GetUsers()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var users = await db.Users
                    .Include(_user =>  _user.Role)
                    .ToListAsync();
                if (users == null)
                {
                    return Content("This collection is not found");
                }

                return Ok(users);
            }
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            using (ApplicationContext db = new ApplicationContext ())
            {
                var user = await db.Users
                    .Include(_user => _user.ChatFirstUser)
                    .ThenInclude(_chat => _chat.SecondUser)
                    .Include(_user => _user.Projects)
                    .SingleOrDefaultAsync(_user => _user.Id == id);
                if (user == null)
                {
                    return Content("Invalid email or Password");
                }

                return Ok(user);
            }
        }
    }
}
