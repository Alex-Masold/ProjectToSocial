using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteMessage(int id)
        {
            using (ApplicationContext db = new ApplicationContext()) 
            {
                Message message = await db.Messages.FirstOrDefaultAsync(_message => _message.Id == id);
                if (message == null)
                {
                    return Content("This user not found");
                }

                db.Messages.Remove(message);
                await db.SaveChangesAsync();
                return Ok(message.Id);
            }
        }

        [HttpPut]
        public async Task<ActionResult<Message>> EditMessage(Message messageData)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                Message message = await db.Messages.FirstOrDefaultAsync(message =>  message.Id == messageData.Id);

                if (message == null)
                {
                    return Content($"Message {messageData.Id} не найдено");
                }
                else
                {
                    message.IdUser = messageData.IdUser;
                    message.IdChat = messageData.IdChat;
                    message.Content = messageData.Content;
                    message.Date = messageData.Date;
                    message.IsEdit = messageData.IsEdit;

                    await db.SaveChangesAsync();
                    return Ok(message);
                }
            }
        }
    }
}
