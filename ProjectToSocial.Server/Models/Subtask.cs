namespace ProjectToSocial.Server.Models
{
    public class Subtask
    {
        public int Id { get; set; }
        public int IdTask { get; set; }
        public int IdUser { get; set; }
        public string? Content { get; set; }
        public bool? Completed { get; set; }

        public virtual Task Task { get; set; } = null!;
        public virtual User User { get; set; } = null!;
    }
}
