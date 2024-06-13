namespace ProjectToSocial.Server.Models
{
    public class Chat
    {
        public int Id { get; set; }
        public int? IdFirstUser { get; set; }
        public int? IdSecondUser { get; set; }

        public User? FirstUser { get; set; }
        public User? SecondUser { get; set; }

        public virtual ICollection<Message> Messages { get; set; } = new List<Message>();

    }
}
