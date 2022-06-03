
class ConnectionMessage {
    MessageType;
    Username;
    UserID;
    IsDrawer;


    constructor(username, userID, isDrawer) {
        this.MessageType = 0;
        this.Username = username;
        this.UserID = userID;
        this.IsDrawer = isDrawer
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
    Base64;


    constructor(base64) {
        this.MessageType = 3;
        this.Base64 = base64;
    }
}