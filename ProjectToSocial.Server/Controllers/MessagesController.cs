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
        public async Task<ActionResult<Message>> AddMessage([FromBody] MassageQuery messageQuery)
        {
            using (ApplicationContext db =  new ApplicationContext())
            {
                Message message = new();
                
                message.IdUser = messageQuery.IdUser;
                message.IdChat = messageQuery.IdChat;
                message.IdProject = messageQuery.IdProject;
                message.Content = messageQuery.Content;
                message.IsEdit = messageQuery.IsEdit;
                message.Date = DateTimeOffset.FromUnixTimeMilliseconds(messageQuery.Date).DateTime.ToLocalTime();

                await db.Messages.AddAsync(message);
                await db.SaveChangesAsync();

                return Ok(message);
            }
        }

        [HttpPut]
        public async Task<ActionResult<Message>> EditMessage(MassageQuery messageData)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                Message message = await db.Messages.FirstOrDefaultAsync(message => message.Id == messageData.Id);

                if (message == null)
                {
                    return Content($"Message {messageData.Id} не найдено");
                }
                else
                {
                    message.IdUser = messageData.IdUser;
                    message.IdChat = messageData.IdChat;
                    message.Content = messageData.Content;
                    message.Date = DateTimeOffset.FromUnixTimeMilliseconds(messageData.Date).DateTime.ToLocalTime();
                    message.IsEdit = messageData.IsEdit;

                    await db.SaveChangesAsync();
                    return Ok(message);
                }
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

        
        public class MassageQuery
        {
            public int Id { get; set; }
            public int? IdUser { get; set; }
            public int? IdChat { get; set; }
            public int? IdProject { get; set; }
            public string? Content { get; set; }
            public bool IsEdit { get; set; }
            public long Date { get; set; }
        }
    }
}
