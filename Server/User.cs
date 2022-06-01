using System.Net.Sockets;

namespace Server;

public class User
{
    private bool _isInit;

    //private int? _userId;


    public static int CommonId = 0;

    public int UserId { get; set; }
    // {
    //     get
    //     {
    //         if (_userId == null)
    //             _userId = GetNewId();

    //         return _userId ?? -1;
    //     }
    //     set => _userId = value;
    // }

    public string Username { get; set; }

    public int[] Position { get; set; }

    public int BombCount { get; set; } = 1;

    public double BombTimer { get; set; }

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


    public static int GetNewId() => ++CommonId;
}