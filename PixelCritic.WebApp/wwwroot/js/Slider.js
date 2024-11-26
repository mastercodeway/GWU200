let next = document.querySelectorAll(".cardNext");
let prev = document.querySelectorAll(".cardPrev");
let cardholder = document.querySelectorAll(".cardslider");
let cards = document.querySelectorAll(".gameCard").length;
var click = [0, 0, 0];
let cardwidth = 311.10;
function moveHolder(index) {
    let offset = click[index] * cardwidth;
    cardholder[index].style.transform = `translateX(${offset}px)`;
}
function updateButton(index, change) {
    console.log(click[index]);
    let viewPosition = cardholder[index].childElementCount - 4
    if (viewPosition < 1) {
        click[index] = 0;
    }
    else if (click[index] <= viewPosition * -1) {
        click[index] = viewPosition * -1;
    } else if (click[index] >= 0) {
        click[index] = 0;
    }
}
function updateButton2(index) {

    if (click[index] == 0) {
        prev[index].disabled = true;
    }
    else {
        prev[index].disabled = false;
    }


    if (click[index] == -cardholder[index].childElementCount + 5) {
        next[index].disabled = true;
    }
    else {
        next[index].disabled = false;
    }
}
for (let i = 0; i < next.length; i++) {

    next[i].addEventListener("click", function () {
        click[i]--;
        console.log(click[i]);
        updateButton(i, -1);
        moveHolder(i);




    });
    prev[i].addEventListener("click", function () {
        click[i]++;
        updateButton(i, 1);
        moveHolder(i);
    });
}