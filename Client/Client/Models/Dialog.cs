namespace Client.Models
{
    class Dialog
    {
        public User User { get; set; }
        public Message LastMessage { get; set; }
        public int DialogId { get; set; }
    }
}
