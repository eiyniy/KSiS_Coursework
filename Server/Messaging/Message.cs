namespace Server.Messaging;

public enum MessageTypes { Connect, Disconnect, Draw, Regular };

public abstract class Message
{
    public MessageTypes MessageType { get; set; }


    protected Message(MessageTypes messageType)
    {
        MessageType = messageType;
    }


    public abstract byte[] ToByteArray();
}
