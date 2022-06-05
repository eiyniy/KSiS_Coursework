let ws = new WebSocket("ws://" + host + ':' + port)


ws.onopen = function () {
    console.log("ws.onopen");

    ws.send(JSON.stringify(new ConnectionMessage(username, userID, isDrawer)));
};

ws.onmessage = function (event) {
    console.log("ws.onmessage");
    console.log("Data recieved: " + event.data);

    let messageRaw = JSON.parse(event.data);
    //console.log("Data message: " + messageRaw);

    switch (messageRaw.MessageType) {
        case 0:
            let cMessage = new ConnectionMessage(messageRaw.Username, messageRaw.UserID, messageRaw.IsDrawer);
            console.log("Connection message: " + cMessage);

            //  не работает с >1 неинициализированным юзером 
            if (userID == -1) {
                userID = cMessage.UserID;
                isDrawer = cMessage.IsDrawer;
            }

            Timer.Start();

            break;

        case 1:
            let dcMessage = new DisconnectionMessage(messageRaw.Username, messageRaw.UserID);
            console.log("Disconnection message: " + dcMessage);

            break;

        case 2:
            let rMessage = new RegularMessage(messageRaw.UserID, messageRaw.Text);
            console.log("Regular message: " + rMessage);

            let s1 = new Date().toLocaleTimeString([], { hour: '2-digit', minute: '2-digit' }) + ' ';
            let s2 = username + ': ';
            let s3 = rMessage.Text + "<br />";
            Output.innerHTML += multyColors(s1, s2, s3);

            break;

        case 3:
            let dMessage = new DrawMessage(messageRaw.Base64);
            console.log("Draw message: " + dMessage);

            let img = new Image();
            img.src = "data:image/png;base64," + dMessage.Base64;

            img.onload = () => {
                ctx.drawImage(img, 0, 0);
            }

            break;

        default:
            break;
    }
};

ws.onerror = function (error) {
    console.log("ws.onerror");
    console.log("Error: " + error.message);
    Timer.Stop();
    ws.close();
};