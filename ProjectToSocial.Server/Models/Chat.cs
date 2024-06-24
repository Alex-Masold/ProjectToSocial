namespace ProjectToSocial.Server.Models
{
    public class Chat
    {
        public int Id { get; set; }

        public ICollection<User> Users { get; set; } = new List<User>();

        public virtual ICollection<Message> Messages { get; set; } = new List<Message>();

    }
}
