namespace Server;

public class Program
{
    public static void Main(string[] args)
    {
        ServerModel.SetDebugMode(true);
        ServerModel.Start("localhost", 11000);

        //var cMessage = new ConnectionMessage(Guid.NewGuid(), "testUN", Color.Yellow);
        //ServerModel.SendMessage();
    }
}
