using System.Net.Sockets;

namespace Server;

public class User
{
    private static int _commonId = 0;
    

    public int UserId { get; set; }

    public string Username { get; set; }

    public Socket Socket { get; set; }


    public User(Socket socket, int userId)
    {
        UserId = userId;
        Socket = socket;
    }


    public static int GetNewId() => ++_commonId;
}