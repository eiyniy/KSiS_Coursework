
class ConnectionMessage {
    MessageType;
    Username;
    UserID;


    constructor(username, userID) {
        this.MessageType = 0;
        this.Username = username;
        this.UserID = userID;
    }
}

class DisconnectionMessage {
    MessageType;
    Username;
    UserID;


    constructor(username, userID) {
        this.MessageType = 1;
        this.Username = username;
        this.UserID = userID;
    }
}

class RegularMessage {
    MessageType;
    Text;
    UserID;


    constructor(userID, text) {
        this.MessageType = 2;
        this.Text = text;
        this.UserID = userID;
    }
}

class DrawMessage {
    MessageType;
    Colors;


    constructor(colors) {
        this.MessageType = 3;
        this.Colors = colors;
    }
}