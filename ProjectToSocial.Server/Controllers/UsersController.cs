using System.Runtime.InteropServices;
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
                var users = await db.Users.Include(_user => _user.Role).ToListAsync();
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
            using (ApplicationContext db = new ApplicationContext())
            {
                var user = await db
                    .Users.Include(_user => _user.Chats)
                    .ThenInclude(_chat => _chat.Users)
                    .Include(_user => _user.Projects)
                    .SingleOrDefaultAsync(_user => _user.Id == id);
                if (user == null)
                {
                    return Content("Invalid email or Password");
                }

                return Ok(user);
            }
        }

        [HttpPut]
        public async Task<ActionResult<Dictionary<string, object>>> EditUser(
            [FromBody] UserQuery userData
        )
                {
            using (ApplicationContext db = new ApplicationContext())
            {
                User user = await db.Users.FirstOrDefaultAsync(user => user.Id == userData.Id);

                bool isModified = false;
                var changedFields = new Dictionary<string, object>();

                if (user == null)
                {
                    return Content($"User {userData.Id} не найден");
                }
                else
                {
                    if (user.IdRole != userData.IdRole && userData.IdRole != null)
                    {
                        user.IdRole = Convert.ToInt32(userData.IdRole);
                        changedFields["IdRole"] = userData.IdRole;
                        isModified = true;
                    }
                    if (
                        user.FirstName != userData.FirstName
                        && !string.IsNullOrEmpty(userData.FirstName)
                    )
                    {
                        user.FirstName = userData.FirstName;
                        changedFields["FirstName"] = userData.FirstName;
                        isModified = true;
                    }
                    if (
                        user.LastName != userData.LastName
                        && !string.IsNullOrEmpty(userData.LastName)
                    )
                    {
                        user.LastName = userData.LastName;
                        changedFields["LastName"] = userData.LastName;
                        isModified = true;
                    }
                    if (user.Family != userData.Family && !string.IsNullOrEmpty(userData.Family))
                    {
                        user.Family = userData.Family;
                        changedFields["Family"] = userData.Family;
                        isModified = true;
                    }
                    if (user.Email != userData.Email && !string.IsNullOrEmpty(userData.Email))
                    {
                        user.Email = userData.Email;
                        changedFields["Email"] = userData.Email;
                        isModified = true;
                    }
                    if (
                        user.Password != userData.Password
                        && !string.IsNullOrEmpty(userData.Password)
                    )
                    {
                        user.Password = userData.Password;
                        changedFields["Password"] = userData.Password;
                        isModified = true;
                    }
                    if (user.Avatar != userData.Avatar && !string.IsNullOrEmpty(userData.Avatar))
                    {
                        user.Avatar = userData.Avatar;
                        changedFields["Avatar"] = userData.Avatar;
                        isModified = true;
                    }

                    if (isModified)
                    {
                        await db.SaveChangesAsync();
                        return Ok(changedFields);
                    }
                    else
                    {
                    return Content("Нет изменений");
                    }
                }
            }
        }

        public class UserQuery
        {
            public int Id { get; set; }
            public int? IdRole { get; set; }
            public string? FirstName { get; set; }
            public string? LastName { get; set; }
            public string? Family { get; set; }
            public string? Email { get; set; }
            public string? Password { get; set; }
            public string? Avatar { get; set; }
        }
    }
}
