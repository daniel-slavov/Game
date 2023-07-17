# Game

Build -> `docker build -t game -f Game.Api/Dockerfile .`

Run -> `docker run -p 5000:5000 -d game`

Test -> `http://localhost:5000/swagger/index.html`