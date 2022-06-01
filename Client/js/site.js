// The port number and hostname of the server.
const port = 11000;
const host = 'localhost';

let username = "Petr";
let userID = -1;

let ws = new WebSocket("ws://" + host + ':' + port)

ws.onopen = function () {
    alert("Соединение установлено.");

    ws.send(JSON.stringify(new ConnectionMessage(username, userID)));
};

ws.onclose = function (event) {
    if (event.wasClean) {
        alert('Соединение закрыто чисто');
    } else {
        alert('Обрыв соединения');
    }
    alert('Код: ' + event.code + ' причина: ' + event.reason);

    let message = new DisconnectionMessage(username, userID);
};

ws.onmessage = function (event) {
    let messageRaw = JSON.parse(event.data);

    switch (messageRaw.MessageType) {
        case 0:
            let message = new ConnectionMessage(messageRaw.Username, messageRaw.UserID);

            //  не работает с >1 неинициализированным юзером 
            if (userID == -1)
                userID = message.UserID;

            alert("1" + message.MessageType + "2" + message.UserID + "3" + message.Username + "\n" + "Получены данные " + event.data);

            break;
    
        default:
            break;
    }
};

ws.onerror = function (error) {
    alert("Ошибка " + error.message);
};
