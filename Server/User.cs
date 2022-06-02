using System.Net.Sockets;

namespace Server;

public class User
{
    private bool _isInit;

    private static int _commonId = 0;
    

    public int UserId { get; set; }

    public string Username { get; set; }

    public Socket Socket { get; set; }


    public User(Socket socket, int userId)
    {
        UserId = userId;
        Socket = socket;

        _isInit = false;
    }

    public User(string username, Socket socket, int userId)
    {
        UserId = userId;
        Username = username;
        Socket = socket;

        _isInit = true;
    }


    public static int GetNewId() => ++_commonId;
}