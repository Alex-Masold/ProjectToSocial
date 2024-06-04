namespace ProjectToSocial.Server.Models
{
    public class User
    {
        public int Id { get; set; }
        public int RoleId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Family {  get; set; }
        public string Email { get; set; }
        public string Pessword { get; set; }

        public Role Role { get; set; }

        public List<Message> Messages { get; set; } = new List<Message>();
        public List<Chat> Chats { get; set; } = new List<Chat>();
        public List<Project> Projects { get; set; } = new List<Project>();
    }
}
