const btn = document.getElementById("add-review");
const cancelbtn = document.querySelector(".modal-cancel-btn");
let bar = document.querySelector(".score-bar");
let scoreBars = document.querySelectorAll(".score-item");
let score = document.querySelector(".show-score");
let hiddenInput = document.querySelector(".modal-hidden-input");
// array med färger; 
let colors = [
    '#9400D3',
    '#9400D3',
    '#A700E5',
    '#A700E5',
    '#BF00F2',
    '#BF00F2',
    '#D500FF',
    '#D500FF',
    '#FF00FF',
    '#FF00FF',
    '#FF66FF'
];
// visar modal. 
btn.addEventListener("click", function () {
    let modal = document.querySelector(".review-modal");
    modal.style.display = "block";

});
// gömmer modal
cancelbtn.addEventListener("click", function () {
    let modal = document.querySelector(".review-modal");
    modal.style.display = "none";
});
// logik som håller koll på vilka knappar är tryckt och sätter gradient på alla fram till det indexet. 
scoreBars.forEach(scoreBar => {
    scoreBar.addEventListener("click", function () {
       
       
        let countScore = parseInt(scoreBar.getAttribute('data-score'));
        console.log(hiddenInput.value);
        
        
        if (hiddenInput.value == countScore) {
            hiddenInput.value = "";
            score.innerHTML = "";
            removeGradient();
        }
        else {
            hiddenInput.value = countScore
            score.innerHTML = countScore;
            removeGradient(); 
            addGradient(countScore);
        }
        
            
           
        
        

    });
});
// add gradient vid hovring och håller koll på score
scoreBars.forEach(scoreBar => {
    scoreBar.addEventListener("mouseover", function () {


        let countScore = parseInt(scoreBar.getAttribute('data-score'));
        score.innerHTML = countScore;
        addGradient(countScore);
    });
});
// tar bort gradient vid hovering
scoreBars.forEach(scoreBar => {
    scoreBar.addEventListener("mouseout", function () {
        if (hiddenInput.value == "") {
            removeGradient();
        }
        else {
            removeGradient();
            score.innerHTML = hiddenInput.value; 
            addGradient(hiddenInput.value);
        }
        
        
    });
});
// lägger till gradient beroende på div index
function addGradient(counter) {
    for (let i = 0; i < counter; i++) {
        let startColor = colors[i];
        let endColor = colors[i + 1];
        scoreBars[i].style.background = `linear-gradient(to right, ${startColor} 50%, ${endColor} 100%)`;
    }
}
// tar bort gradient från div item. 
function removeGradient() {
    scoreBars.forEach(bar => {
        bar.style.background = "#333";
    });
}

