using System.Text;
using System.Text.Json;

namespace Server.Messaging;
public class PlaceBombMessage : Message
{
    

    public PlaceBombMessage(): base(MessageTypes.PlaceBomb)
    {

    }

     public override byte[] ToByteArray() =>
        Encoding.UTF8.GetBytes(JsonSerializer.Serialize(this));
}
