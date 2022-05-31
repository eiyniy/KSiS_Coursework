using System.Drawing;

namespace Server;

public class User
{
    private static int _commonId = 0;
    private int _id;
    private string _name;
    private bool _isDrawer;
    

    public User(string name, bool isDrawer)
    {
        _id = GetNewId();
        _name = name;
        _isDrawer = isDrawer;
    }


    public static int GetNewId() => ++_commonId;
}