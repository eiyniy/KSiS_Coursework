using System.Net.Sockets;
using System.Text.Json.Serialization;

namespace Server;

public class User
{
    private static int _commonId = 0;

    public int UserID { get; set; }

    public string Username { get; set; }

    public int PositionX { get; set; }

    public int PositionY { get; set; }

    [JsonIgnore]
    public int BombCount { get; set; } = 1;

    [JsonIgnore]
    public double BombTimer { get; set; }

    [JsonIgnore]
    public Socket Socket { get; set; }


    public User(Socket socket, int userID)
    {
        UserID = userID;
        Socket = socket;
    }


    public static int GetNewId() => ++_commonId;
}