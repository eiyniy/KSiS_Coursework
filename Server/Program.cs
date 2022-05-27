namespace Server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            ServerModel.Start("localhost", 11000);
        }
    }
}
