// The port number and hostname of the server.
const port = 11000;
const host = 'localhost';

let username = "Petr";
let userID = -1;

let ws = new WebSocket("ws://" + host + ':' + port)


ws.onopen = function () {
    alert("Соединение установлено.");

    ws.send(JSON.stringify(new ConnectionMessage(username, userID, )));
};

ws.onmessage = function (event) {
    alert("Получены данные " + event.data);

    let messageRaw = JSON.parse(event.data);

    switch (messageRaw.MessageType) {
        case 0:
            let cMessage = new ConnectionMessage(messageRaw.Username, messageRaw.UserID);

            //  не работает с >1 неинициализированным юзером 
            if (userID == -1)
            {
                userID = cMessage.UserID;
                ws.send(JSON.stringify(new RegularMessage(userID, "Hello Server!")));
            }

            break;

        case 1:
            let dcMessage = new DisconnectionMessage(messageRaw.Username, messageRaw.UserID);

            break;

        case 2:
            let rMessage = new RegularMessage(messageRaw.UserID, messageRaw.Text);
            
            break;

        case 3:
            let dMessage = new DrawMessage(messageRaw.Colors);

            break;

        default:
            break;
    }
};

ws.onerror = function (error) {
    alert("Ошибка " + error.message);
};

window.onunload = function () {
    ws.send(JSON.stringify(new DisconnectionMessage(username, userID)));
    ws.close();
}