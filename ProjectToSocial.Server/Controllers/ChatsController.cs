using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectToSocial.Server.DataContext;
using ProjectToSocial.Server.Models;

namespace ProjectToSocial.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatsController : ControllerBase
    {
        [HttpGet("{id}")]
        public async Task<ActionResult<Chat>> GetChat (int id)
        {
            using (ApplicationContext db =  new ApplicationContext())
            {
                var chat = await db.Chats
                    .Include(_chat => _chat.Users)
                    .Include(_chat => _chat.Messages)
                    .SingleOrDefaultAsync(_chat => _chat.Id == id);
                if (chat == null)
                {
                    return Content("This Chat is not found");
                }

                return Ok(chat);
            }
        }
    }
}
    