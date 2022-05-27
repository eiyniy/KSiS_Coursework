using System.Reflection;

namespace Library.Messaging;

public abstract class Message
{
    protected readonly byte[] _messageTypes = {
        1,  //connect
        2,  //disconnect
    };


    public byte MessageType { get; set; }


    public abstract byte[] ToByteArray();
}
