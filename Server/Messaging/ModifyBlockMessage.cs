using System.Drawing;
using System.Text;
using System.Text.Json;

namespace Server.Messaging;

[Serializable]
public class ModifyBlockMessage : Message
{
    public int SoftBlockX { get; set; }

    public int SoftBlockY { get; set; }


    public ModifyBlockMessage(Point softBlock) : base(MessageTypes.ModifyBlock)
    {
        SoftBlockX = softBlock.X;
        SoftBlockY = softBlock.Y;
    }


    public override byte[] ToByteArray() =>
        Encoding.UTF8.GetBytes(JsonSerializer.Serialize(this));
}