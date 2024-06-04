using ProjectToSocial.Server.Models.interfaces;

namespace ProjectToSocial.Server.Models
{
    public class Project : ICommunication
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<User> Users { get; set; } = new List<User>();
        public List<Task> Tasks { get; set; } = new List<Task>();
        public List<Message> Messages { get; set; }  = new List<Message>();
    }
}
