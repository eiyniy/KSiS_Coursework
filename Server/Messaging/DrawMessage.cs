using System.Drawing;
using System.Text;
using System.Text.Json;

namespace Server.Messaging;

public class DrawMessage : Message
{
    public Color[][] _canvas; 


    public DrawMessage(Color[][] canvas) 
        : base(MessageTypes.Draw)
    {
        _canvas = canvas;
    }


    public override byte[] ToByteArray() =>
        Encoding.UTF8.GetBytes(JsonSerializer.Serialize(this));
}
