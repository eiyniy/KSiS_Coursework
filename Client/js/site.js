import * as Messages from "./Messages";

// The port number and hostname of the server.
const port = 11000;
const host = 'localhost';

let ws = new WebSocket("ws://" + host + ':' + port)

ws.onopen = function () {
    alert("Соединение установлено.");

    ws.send(JSON.stringify(new Messages.ConnectionMessage("#FFFFFF", "Vanya", false)));
};

ws.onclose = function (event) {
    if (event.wasClean) {
        alert('Соединение закрыто чисто');
    } else {
        alert('Обрыв соединения');
    }
    alert('Код: ' + event.code + ' причина: ' + event.reason);
};

ws.onmessage = function (event) {
    alert("Получены данные " + event.data);

    JSON.parse();
};

ws.onerror = function (error) {
    alert("Ошибка " + error.message);
};
