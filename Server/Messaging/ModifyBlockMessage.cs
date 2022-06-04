using System.Drawing;
using System.Text;
using System.Text.Json;

namespace Server.Messaging;

[Serializable]
public class ModifyBlockMessage : Message
{
    public int PositionX { get; set; }

    public int PositionY { get; set; }

    public bool IsDelete { get; set; }
    public ModifyBlockMessage(int posX, int posY, bool isDelete) : base(MessageTypes.ModifyBlock)
    {
        PositionX = posX;
        PositionY = posY;
        IsDelete = isDelete;
    }


    public override byte[] ToByteArray() =>
        Encoding.UTF8.GetBytes(JsonSerializer.Serialize(this));
}