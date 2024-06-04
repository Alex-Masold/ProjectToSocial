namespace ProjectToSocial.Server.Models.interfaces
{
    public interface ICommunication
    {
        public int Id { get; set; }
        public List<Message> Messages { get; set; }
    }
}
