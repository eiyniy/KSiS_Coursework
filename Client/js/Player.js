class Player {
    UserID;
    Username;
    PositionX;
    PositionY;
    BombCount;
    
    
    constructor(userID, username, positionX, positionY){
        this.UserID = userID;
        this.Username = username;
        this.PositionX = positionX;
        this.PositionY = positionY;
    }

    RenderPlayer() {
        const x = (this.col + 0.5) * grid;
        const y = (this.row + 0.5) * grid; 

        context.save();
        context.fillStyle = 'white';
        context.beginPath();
        context.arc(x, y, grid * 0.35, 0, 2 * Math.PI);
        context.fill();
    }
}