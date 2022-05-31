using Server.Messaging;

namespace Server;

public class Program
{
    public static void Main(string[] args)
    {
        ServerModel.SetDebugMode(true);
        ServerModel.Start("localhost", 11000);

        while (true)
        {
            var message = new ConnectionMessage(User.GetNewId(), "Man");
            ServerModel.SendAll(message);
            Console.ReadKey();
        }

        //var cMessage = new ConnectionMessage(Guid.NewGuid(), "testUN", Color.Yellow);
        //ServerModel.SendMessage();
    }
}
