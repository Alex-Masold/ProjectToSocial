using ProjectToSocial.Server.Models.interfaces;
using System.Diagnostics.Contracts;

namespace ProjectToSocial.Server.Models
{
    public class Message
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ChatId { get; set; }
        public string Text {  get; set; }
        public DateTime Date {  get; set; }

        public User User { get; set; }
        public ICommunication Chat { get; set; }    
    }
}
