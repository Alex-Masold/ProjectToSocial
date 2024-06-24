using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectToSocial.Server.DataContext;
using ProjectToSocial.Server.Models;

namespace ProjectToSocial.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessagesController : ControllerBase
    {
        [HttpPost]
        public async Task<ActionResult<Message>> AddMessage([FromBody] Message message)
        {
            using (ApplicationContext db =  new ApplicationContext())
            {
                await db.Messages.AddAsync(message);
                await db.SaveChangesAsync();

                return Ok(message);
            }
        }
    }
}
