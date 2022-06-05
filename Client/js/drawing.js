let canvas = document.getElementById("canvas");
let ctx = canvas.getContext("2d");

let mouseX = 0;
let mouseY = 0;

let color = 'black';

// Стиль линии
ctx.lineWidth = 1;
let isDrawing = false;

// Обработчики рисования мышкой
canvas.addEventListener('mousedown', function (event) {
    if (isDrawer) {
        setMouseCoordinates(event);
        isDrawing = true;
        ctx.beginPath();
        ctx.moveTo(mouseX, mouseY);
    }
});

canvas.addEventListener('mousemove', function (event) {
    if (isDrawer) {
        setMouseCoordinates(event);

        if (isDrawing) {
            ctx.lineTo(mouseX, mouseY);
            ctx.stroke();
        }
    }
});

canvas.addEventListener('mouseup', function (event) {
    if (isDrawer) {
        setMouseCoordinates(event);
        isDrawing = false;
    }
});

function setMouseCoordinates(event) {
    mouseX = event.offsetX;
    mouseY = event.offsetY;
}

let colorInput = document.getElementById("colorInput");

colorInput.addEventListener('input', colorChange);
colorInput.addEventListener('change', colorChange);

function colorChange(event) {
    color = colorInput.value;
    ctx.strokeStyle = color;
}