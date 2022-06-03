// The port number and hostname of the server.
const port = 11000;
const host = 'localhost';

let username = "Petr";
let userID = -1;
let isDrawer = false;

window.onunload = function () {
    console.log("ws.onunload");
    ws.send(JSON.stringify(new DisconnectionMessage(username, userID)));
    ws.close();
}