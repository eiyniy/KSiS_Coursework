using System.Text;
using System.Text.Json;
using static Server.Map;

namespace Server.Messaging
{
    public class CreateMapMessage : Message
    {
        public CellTypes[,] MapTemplate;


        public CreateMapMessage() : base(MessageTypes.CreateMap)
        {
            Map.GenerateMap();
            MapTemplate = Map.MapTemplate;
        }


        public override byte[] ToByteArray() =>
            Encoding.UTF8.GetBytes(JsonSerializer.Serialize(this));
    }
}