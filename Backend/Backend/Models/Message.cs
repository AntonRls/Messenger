namespace Backend.Models
{
    public class Message
    {
        public int MessageId { get; set; }
        public User From { get; set; }
        public User To { get; set; }
        public string Text { get; set; }
        public long TimeCreate { get; set; }
        public int DialogId { get; set; }
    }
}
