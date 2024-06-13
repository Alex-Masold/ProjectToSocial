
namespace ProjectToSocial.Server.Models
{
    public class Project
    {
        public int Id { get; set; }
        public string? Name { get; set; }

        public ICollection<User>? Users { get; set; } = new List<User>();
        public ICollection<Task>? Tasks { get; set; } = new List<Task>();
        public ICollection<Message>? Messages { get; set; }  = new List<Message>();
    }
}
