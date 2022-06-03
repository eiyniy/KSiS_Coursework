using System.Text;
using System.Text.Json;

namespace Server.Messaging;

public class DrawMessage : Message
{
    public string Base64 { get; set; }


    public DrawMessage(string base64)
        : base(MessageTypes.Draw)
    {
        Base64 = base64;
    }


    public override byte[] ToByteArray() =>
        Encoding.UTF8.GetBytes(JsonSerializer.Serialize(this));
}
