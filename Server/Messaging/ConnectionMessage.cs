using System.Text;
using System.Drawing;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Server.Messaging;

public class ConnectionMessage : Message
{
    public int UserID { get; set; }
    public string Username { get; set; }
    public DateTime Time { get; set; }


    public ConnectionMessage(int userID, string username)
        : base(MessageTypes.Connect)
    {
        UserID = userID;
        Username = username;
        Time = DateTime.Now;
    }


    public override byte[] ToByteArray() => Encoding.UTF8.GetBytes(JsonSerializer.Serialize(this));
}