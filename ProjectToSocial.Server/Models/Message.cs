namespace ProjectToSocial.Server.Models
{
    public class Message
    {
        public int Id { get; set; }
        public int? IdUser { get; set; }
        public int? IdChat { get; set; }
        public int? IdProject { get; set; }
        public string? Content {  get; set; }
        public DateTime? Time {  get; set; }

        public virtual User? User { get; set; }
        public virtual Chat? Chat { get; set; }
        public virtual Project? Project { get; set; }
    }
}
