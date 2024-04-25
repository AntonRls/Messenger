namespace Client.Models
{
    public class Message
    {
        public int MessageId { get; set; }
        public User From { get; set; }
        public User To { get; set; }
        public string Text { get; set; }
        public int TimeCreate { get; set; }
        public DateTime DateTimeCreate
        {
            get
            {
                return DateTimeOffset.FromUnixTimeSeconds(TimeCreate).DateTime;
            }
        }
        public string HourAndMinuteMessage
        {
            get
            {
                string hour = DateTimeCreate.Hour.ToString();
                string minute = DateTimeCreate.Minute.ToString();
                if (hour.Length == 1)
                    hour = "0" + hour;
                if (minute.Length == 1)
                    minute = "0" + minute;
                return hour + " : " + minute;
            }
        }
        public int DialogId { get; set; }
    }
}
