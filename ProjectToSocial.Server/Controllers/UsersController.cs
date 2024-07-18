using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ProjectToSocial.Server.DataContext;
using ProjectToSocial.Server.Models;

namespace ProjectToSocial.Server.Controllers
{
    [Route("api")]
    [ApiController]
    public partial class UsersController : ControllerBase
    {
        [HttpGet("users")]
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

        [HttpGet("user/{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var user = await db.Users
                    .Include(_user => _user.Chats)
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

        [HttpPut("user")]
        public async Task<ActionResult<Dictionary<string, object>>> EditUser(
            [FromBody] UserQuery userData
        )
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                User user = await db.Users.FirstOrDefaultAsync(user => user.Id == userData.Id);

                var changedFields = new Dictionary<string, object>();

                if (user == null)
                {
                    return Content($"User {userData.Id} не найден");
                }

                if (userData.IdRole != null)
                {
                    if (userData.IdRole == 0)
                    {
                        ModelState.AddModelError(nameof(UserQuery.IdRole), "Пустое значение");
                    }
                    if (user.IdRole.Equals(userData.IdRole))
                    {
                        ModelState.AddModelError(nameof(UserQuery.IdRole), "Были введены старые данные");
                    }
                    if (user.IdRole >= db.Roles.Count())
                    {
                        ModelState.AddModelError(nameof(UserQuery.IdRole), "Значение отсутствует");
                    }
                    if (ModelState.IsValid)
                    {
                        user.IdRole = Convert.ToInt32(userData.IdRole);
                        changedFields[nameof(UserQuery.IdRole)] = userData.IdRole;
                    }
                }
                if (!userData.FirstName.IsNullOrEmpty() ||
                    !userData.LastName.IsNullOrEmpty()  ||
                    !userData.Family.IsNullOrEmpty())
                {
                    if ($"{userData.FirstName}{userData.LastName}{userData.Family}" == $"{user.FirstName}{user.LastName}{user.Family}")
                    {
                        ModelState.AddModelError("", "Были введены старые данные");
                    }
                    if (ModelState.IsValid)
                    {
                        if (user.FirstName != userData.FirstName)
                        {
                            user.FirstName = userData.FirstName;
                            changedFields[nameof(UserQuery.FirstName)] = userData.FirstName;
                        }
                        if (user.LastName != userData.LastName)
                        {
                            user.LastName = userData.LastName;
                            changedFields[nameof(UserQuery.LastName)] = userData.LastName;
                        }
                        if (user.Family != userData.Family)
                        {
                            user.Family = userData.Family;
                            changedFields[nameof(UserQuery.Family)] = userData.Family;
                        }
                    }
                }


                if (!string.IsNullOrEmpty(userData.Email))
                {
                    if (userData.Email == user.Email)
                    {
                        ModelState.AddModelError(nameof(UserQuery.Email), "Был введен старыц Email");
                    }
                    if (db.Users.Any(user => user.Email == userData.Email))
                    {
                        ModelState.AddModelError(nameof(UserQuery.Email), "Email Уже занят");
                    }
                    if (ModelState.IsValid)
                    {
                        user.Email = userData.Email;
                        changedFields[nameof(UserQuery.Email)] = userData.Email;
                    }
                }
                if (!string.IsNullOrEmpty(userData.Password))
                {
                    if (userData.Password == user.Password)
                    {
                        ModelState.AddModelError(nameof(UserQuery.Password), "Нельзя использовать старый пароль");
                    }
                    if (
                        userData.Password == $"{user.FirstName}{user.LastName}{user.Family}"||
                        userData.Password == user.FirstName ||
                        userData.Password == user.LastName ||
                        userData.Password == user.Family ||
                        userData.Password == $"{userData.FirstName}{userData.LastName}{userData.Family}"||
                        userData.Password == userData.FirstName ||
                        userData.Password == userData.LastName ||
                        userData.Password == userData.Family)
                    {
                        ModelState.AddModelError("", "Пароль не должен быть равен Имени");
                    }
                    if (ModelState.IsValid)
                    {
                        user.Password = userData.Password;
                        changedFields[nameof(UserQuery.Password)] = userData.Password;
                    }

                }
                if (user.Avatar != userData.Avatar && !string.IsNullOrEmpty(userData.Avatar))
                {
                    user.Avatar = userData.Avatar;
                    changedFields[nameof(UserQuery.Avatar)] = userData.Avatar;
                }

                if (ModelState.IsValid)
                {
                    await db.SaveChangesAsync();
                    return Ok(changedFields);
                }
                else
                {
                    return BadRequest(new ValidationProblemDetails(ModelState));
                }

            }
        }
    }
}
