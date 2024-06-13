namespace ProjectToSocial.Server.Models
{
    public class User
    {
        public int Id { get; set; }
        public int IdRole { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Family {  get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }

        public Role Role { get; set; }

        public virtual ICollection<Chat> ChatFirstUser { get; set; } = new List<Chat>();
        public virtual ICollection<Chat> ChatSecondUser { get; set; } = new List<Chat>();
        public virtual ICollection<Message> Messages { get; set; } = new List<Message>();
        public virtual ICollection<Subtask> Subtasks { get; set; } = new List<Subtask>();
        public virtual ICollection<Project> Projects { get; set; } = new List<Project>();
        public virtual ICollection<Task> Tasks { get; set; } = new List<Task>();
    }
}
