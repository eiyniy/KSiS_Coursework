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
        //debugger;
        const x = (this.PositionX + 0.5) * grid;
        const y = (this.PositionY + 0.5) * grid; 

        context.save();
        context.fillStyle = 'white';
        context.beginPath();
        context.arc(x, y, grid * 0.35, 0, 2 * Math.PI);
        context.fill();
    }
}