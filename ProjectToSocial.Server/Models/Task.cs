namespace ProjectToSocial.Server.Models
{
    public class Task
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public int UserId { get; set; }
        public string Title { get; set; }
        public bool Comleted { get; set; }

        public Project Project { get; set; }
        public User User { get; set; }      
    }
}
