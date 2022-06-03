
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

class MoveMessage  {
    MessageType;
    Username;
    UserID;
    PositionX;
    PositionY;

    constructor(username, userID, positionX, positionY){
        this.MessageType = 2;
        this.UserID = userID;
        this.Username = username;
        this.PositionX = positionX;
        this.PositionY = positionY;
    }
}


const template = [
    ['w', 'w', 'w', 'w', 'w', 'w', 'w', 'w', 'w', 'w', 'w', 'w', 'w', 'w', 'w', 'w', 'w', 'w', 'w'],
    ['w', , , , , , , , , , , , , , , , , , 'w'],
    ['w', , 'w', , 'w', , 'w', , 'w', , 'w', , 'w', , 'w', , 'w', , 'w'],
    ['w', , , , , , , , , , , , , , , , , , 'w'],
    ['w', , 'w', , 'w', , 'w', , 'w', , 'w', , 'w', , 'w', , 'w', , 'w'],
    ['w', , , , , , , , , , , , , , , , , , 'w'],
    ['w', , 'w', , 'w', , 'w', , 'w', , 'w', , 'w', , 'w', , 'w', , 'w'],
    ['w', , , , , , , , , , , , , , , , , , 'w'],
    ['w', , 'w', , 'w', , 'w', , 'w', , 'w', , 'w', , 'w', , 'w', , 'w'],
    ['w', , , , , , , , , , , , , , , , , , 'w'], 
    ['w', 'w', 'w', 'w', 'w', 'w', 'w', 'w', 'w', 'w', 'w', 'w', 'w', 'w', 'w', 'w', 'w', 'w', 'w']
];

const types = {
    wall: 'w',
    softWall: 1,
    bomb: 2
};


function generateMap(pointX, pointY) {
    //console.log(pointX + " " + pointY + "| ");
    template[pointX][pointY] = types.softWall;
}
