using System.Text;
using System.Drawing;
using System.Text.Json;

namespace Server.Messaging;

public class ConnectionMessage : Message
{
    private Guid _userID;
    private string _username;
    private Color _userColor;
    private DateTime _time;


    public ConnectionMessage(Guid userID, string username, Color userColor)
        : base(MessageTypes.Connect)
    {
        _userID = userID;
        _username = username;
        _userColor = userColor;
        _time = DateTime.Now;
    }


    public override byte[] ToByteArray() => Encoding.UTF8.GetBytes(JsonSerializer.Serialize(this));
}