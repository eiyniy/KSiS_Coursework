// The port number and hostname of the server.
const port = 11000;
const host = 'localhost';

let ws = new WebSocket("ws://" + host + ':' + port)

ws.onopen = function () {
    alert("Соединение установлено.");

    ws.send("Hello Server!");
};

ws.onclose = function (event) {
    if (event.wasClean) {
        alert('Соединение закрыто чисто');
    } else {
        alert('Обрыв соединения'); // например, "убит" процесс сервера
    }
    alert('Код: ' + event.code + ' причина: ' + event.reason);
};

ws.onmessage = function (event) {
    alert("Получены данные " + event.data);
};

ws.onerror = function (error) {
    alert("Ошибка " + error.message);
};