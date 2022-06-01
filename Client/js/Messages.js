
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

