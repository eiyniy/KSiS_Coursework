
Input = document.getElementById("input");
Output = document.getElementById("output");

Input.addEventListener("keyup", inputHandler);

function inputHandler(e) {
    if (e.key != "Enter")
        return;

    let rm = JSON.stringify(new RegularMessage(userID, Input.value));
    //alert(rm);
    ws.send(rm);

    Input.value = "";
}

function multyColors(s1, s2, s3) {
    s1 = "<span style=\"color: #6e6e6e; font-size: 12px\">" + s1 + "</span>";
    s2 = "<span style=\"color: #6e6e6e; font-size: 12px\">" + s2 + "</span>";
    s3 = "<span>" + s3 + "</span>";

    return(s1 + s2 + s3);
}