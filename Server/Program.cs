namespace Server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            ServerModel.SetDebugMode(true);
            ServerModel.Start("localhost", 11000);
        }
    }
}
