class Timer {
    DrawTimer;


    static Start() {
        let canvas = document.getElementById("canvas");
        let base64;
        let oldBase64;

        this.DrawTimer = setInterval(() => {
            //console.log("timer tick");

            if (!isDrawer)
                return;

            //console.log("Base64 recalculate");
            oldBase64 = base64;
            base64 = canvas.toDataURL().replace(/^data:image\/png;base64,/, "");

            if (oldBase64 != base64) {
                let jsonBase64 = JSON.stringify(new DrawMessage(base64));

                console.log("Base64: " + jsonBase64);
                ws.send(jsonBase64);
            }
        }, 1000);
    }

    static Stop()
    {
        clearInterval(this.DrawTimer);
    }
}
