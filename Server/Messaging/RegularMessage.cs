using System.Text;
using System.Text.Json;

namespace Server.Messaging
{
    public class RegularMessage : Message
    {
        public int UserID { get; set; }
        public string Text { get; set; }
        public DateTime Time { get; set; }


        public RegularMessage(int userID, string text)
            : base(MessageTypes.Regular)
        {
            UserID = userID;
            Text = text;
            Time = DateTime.Now;
        }


        public override byte[] ToByteArray() =>
            Encoding.UTF8.GetBytes(JsonSerializer.Serialize(this));
    }
}