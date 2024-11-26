let section = document.querySelectorAll(".cardslider");
const requestOptions = {
    method: "GET",
    redirect: "follow"
};

let mostRelevant = null; 
let latestGames = null;
// https://learn.microsoft.com/en-us/aspnet/core/tutorials/web-api-javascript?view=aspnetcore-8.0
// hämtar data från backend. 
function fetchGames() {
    fetch("/Game/CallGamesApi", requestOptions)
        .then((response) => {

            if (!response.ok) {
                throw new Error('Network response was not ok: ' + response.statusText);
            }
            return response.json();
        })
        .then((result) => {
             // slicer resultat så får 20 st från json och sedan sorterar efter datum
            displayMostRelevantGames(result.slice(0, 20));
            return result.sort((a, b) => new Date(b.release_date) - new Date(a.release_date));
            
            
        })
        .then((latest) => {
            displayLatestGames(latest.slice(0,20)); 
        })
        .catch((error) => console.error('Error:', error));
}

// Skapar card på homeScreen dynamisk genom att gå igenom alla spel. 
function displayMostRelevantGames(games) {
    if (mostRelevant) {
        return;
    }
    
    mostRelevant = games;
    mostRelevant.forEach(game => {
        let card =`<a href="/Game/Game?gameData=${encodeURIComponent(JSON.stringify(game))}">
            <div class="gameCard">
                <div class="gameCardImgContainer">
                    <img src="${game.thumbnail}" alt="" />
                </div>
                <div class="gameTitel">
                    <h2>${game.title}</h2>
                </div>
                <h3 class="scoreTitel">Score</h3>
                <h3 class="reviewMade">Based on ${game.number_of_reviews} reviews</h3>
                <div class="Score">
                    <h2>${game.rating}</h2>
                </div>
            </div>
        </a>`;
        section[0].innerHTML += card;
    });
}

// Samma som ovan fast annan sektion. 

function displayLatestGames(games) {
    if (latestGames) {
        return;
    }

    latestGames = games;
    latestGames.forEach(game => {
        let card = `<a href="/Game/Game?gameData=${encodeURIComponent(JSON.stringify(game))}">
            <div class="gameCard">
                <div class="gameCardImgContainer">
                    <img src="${game.thumbnail}" alt="" />
                </div>
                <div class="gameTitel">
                    <h2>${game.title}</h2>
                </div>
                <h3 class="scoreTitel">Score</h3>
                <h3 class="reviewMade">Based on ${game.number_of_reviews} reviews</h3>
                <div class="Score">
                    <h2>${game.rating}</h2>
                </div>
            </div>
        </a>`;
        section[1].innerHTML += card;
    });
}


fetchGames();