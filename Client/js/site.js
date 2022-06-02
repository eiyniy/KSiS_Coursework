const canvas = document.getElementById('game');
canvas.width = innerWidth * 0.845;
canvas.height = innerHeight * 0.859;
// содержимое холста
const context = canvas.getContext('2d');
// размеры одной клетки, количество строк и столбцов
const grid = 64;
const numRows = 11;
const numCols = 19;

// создаём кирпичные стены, которые потом расставим по всему полю и будем взрывать. На них будет кирпичный рисунок. Наша задача — нарисовать на стене этот рисунок.
const softWallCanvas = document.createElement('canvas');
const softWallCtx = softWallCanvas.getContext('2d');
// размер квадратика стены равен размеру клетки игрового поля
softWallCanvas.width = softWallCanvas.height = grid;

// цвет швов между кирпичами
softWallCtx.fillStyle = 'black';
// закрашиваем ими всю клетку
softWallCtx.fillRect(0, 0, grid, grid);
// цвет кирпича
softWallCtx.fillStyle = '#a9a9a9';

// первый ряд кирпичей 
softWallCtx.fillRect(1, 1, grid - 2, 20);
// второй ряд кирпичей
softWallCtx.fillRect(0, 23, 20, 18);
softWallCtx.fillRect(22, 23, 42, 18);
// третий ряд кирпичей
softWallCtx.fillRect(0, 43, 42, 20);
softWallCtx.fillRect(44, 43, 20, 20);

// теперь создадим неразрушаемые блоки — их нельзя будет уничтожить
const wallCanvas = document.createElement('canvas');
const wallCtx = wallCanvas.getContext('2d');
// тоже размером с игровую клетку
wallCanvas.width = wallCanvas.height = grid;

// цвет тени
wallCtx.fillStyle = 'black';
wallCtx.fillRect(0, 0, grid, grid);
// цвет верхнего освещения — для объёма
wallCtx.fillStyle = 'white';
wallCtx.fillRect(0, 0, grid - 2, grid - 2);
// цвет стены
wallCtx.fillStyle = '#a9a9a9';
wallCtx.fillRect(2, 2, grid - 4, grid - 4);





// The port number and hostname of the server.
const port = 11000;
const host = 'localhost';



let username = "Player";
let userID = -1;



let ws = new WebSocket("ws://" + host + ':' + port)

ws.onopen = function () {
    alert("Соединение установлено.");

    ws.send(JSON.stringify(new ConnectionMessage(username, userID)));
};

ws.onmessage = function (event) {


    let messageRaw = JSON.parse(event.data);

    switch (messageRaw.MessageType) {
        case 0:
            alert("Получены данные " + event.data);
            let cMessage = new ConnectionMessage(messageRaw.Username, messageRaw.UserID);

            //  не работает с >1 неинициализированным юзером (или нет) 
            if (userID == -1)
                userID = cMessage.UserID;

            break;

        case 1:
            alert("Получены данные " + event.data);
            let dMessage = new DisconnectionMessage(messageRaw.Username, messageRaw.UserID);

            break;

        case 2:
            break;

        case 3:
            generateMap(messageRaw.SoftBlockX, messageRaw.SoftBlockY);
            drawMap();

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




function generateLevel() {
    // на старте пока уровень пустой
    cells = [];

    // cначала считаем строки
    for (let row = 0; row < numRows; row++) {
        cells[row] = [];

        // потом столбцы
        for (let col = 0; col < numCols; col++) {
            if (template[row][col] === types.wall) {
                cells[row][col] = types.wall;
            }
        }
    }
}

function drawMap() {
    for (let row = 0; row < numRows; row++) {
        for (let col = 0; col < numCols; col++) {
            switch (template[row][col]) {
                case types.wall:
                    context.drawImage(wallCanvas, col * grid, row * grid);
                    break;
                case types.softWall:
                    context.drawImage(softWallCanvas, col * grid, row * grid);
                    break;
            }
        }
    }
}






// ws.onclose = function (event) {
//     if (event.wasClean) {
//         alert('Соединение закрыто чисто');
//     } else {
//         alert('Обрыв соединения');
//     }
//     alert('Код: ' + event.code + ' причина: ' + event.reason);

//     ws.send(JSON.stringify(new DisconnectionMessage(username, userID)));
// };
