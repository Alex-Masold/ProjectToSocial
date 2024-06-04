using ProjectToSocial.Server.Models.interfaces;

namespace ProjectToSocial.Server.Models
{
    public class Chat : ICommunication
    {
        public int Id { get; set; }
        public int FirstUserId { get; set; }
        public int SecondUserId { get; set; }

        public User FirstUser { get; set; }
        public User SecondUser { get; set; }

        public List<Message> Messages { get; set; } = new List<Message>();

    }
}
