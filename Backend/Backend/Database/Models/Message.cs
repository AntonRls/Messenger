namespace Backend.Database.Models
{
    public class Message
    {
        public int MessageId { get; set; }
        public int DialogId { get; set; }
        public int ToUser { get; set; }
        public int FromUser { get; set; }
        public string Text { get; set; }
        public long TimeCreate { get; set; }

    }
}
