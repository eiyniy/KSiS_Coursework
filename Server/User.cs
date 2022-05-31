using System.Drawing;

namespace Server;

public class User
{
    private static int _commonId = 0;
    private int _id;
    private Color _messageColor;
    private string _name;
    private bool _isDrawer;
    

    public User(string name, bool isDrawer, Color messageColor)
    {
        _id = GetNewId();
        _messageColor = messageColor;
        _name = name;
        _isDrawer = isDrawer;
    }


    public static int GetNewId() => ++_commonId;
}