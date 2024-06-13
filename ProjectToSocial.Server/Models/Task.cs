namespace ProjectToSocial.Server.Models
{
    public class Task
    {
        public int Id { get; set; }
        public int IdProject { get; set; }
        public string? Content { get; set; }
        public bool? Completed { get; set; }

        public virtual Project Project { get; set; }
        public virtual ICollection<Subtask> Subtasks { get; set; } = new List<Subtask>();   
        public virtual ICollection<User> Users { get; set; } = new List<User>();
    }
}
