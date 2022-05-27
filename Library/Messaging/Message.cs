namespace Library.Messaging;

public abstract class Message
{
    protected readonly byte[] _messageTypes = {
        1,  //connect
        2,  //disconnect
        3,  //draw
        4,  //message
    };


    public byte MessageType { get; set; }


    public abstract byte[] ToByteArray();
}
