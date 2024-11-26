const slides = document.querySelector('.slidesContainer');
const slideCount = document.querySelectorAll('.slidesItem').length;
let currentIndex = 0;
let intervalId;
const delay = 3000;

// Förflyttar kortet i x led 100 gånger vilket index kortet ligger på. 
function showSlide(index) {
    const offset = -index * 100;
    slides.style.transform = `translateX(${offset}%)`;
}

// räknar ut när kortet behöver gå tillbaka till index 0. 
function nextSlide() {
    currentIndex = (currentIndex + 1) % slideCount;

    showSlide(currentIndex);
}

function prevSlide() {

    currentIndex = (currentIndex - 1 + slideCount) % slideCount;
    showSlide(currentIndex);
}
// sätter tid för hur ofta funktionen ska anropas. 
function startAutoSlide() {

    intervalId = setInterval(nextSlide, delay);
}

function stopAutoSlide() {
    clearInterval(intervalId);
}
function delayStartAutoSlide() {
    stopAutoSlide();
    setTimeout(startAutoSlide(), delay);
}


document.querySelector('.next').addEventListener('click', () => {


    delayStartAutoSlide();
});

document.querySelector('.prev').addEventListener('click', () => {


    delayStartAutoSlide();

});

// stoppar karusell vid hovering och startar igen efter släppt hoveringen. 
startAutoSlide();
document.querySelector('.heroHeader').addEventListener('mouseover', stopAutoSlide);
document.querySelector('.heroHeader').addEventListener('mouseout', delayStartAutoSlide);