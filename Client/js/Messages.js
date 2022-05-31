
export class ConnectionMessage {
    messageColor;
    name;
    isDrawer;


    FromJSON(message)
    {
        let data = JSON.parse(message);
        return data;
    }


    constructor(messageColor, name, isDrawer)
    {
        this.messageColor = messageColor;
        this.name = name;
        this.isDrawer = isDrawer;
    }
}