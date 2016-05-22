# Mastermind VanHackathon API v1.0

### Overview

This API works in 2 different modes:

1. Single Player
2. Multiplayer

## Single Player Mode

Create a new game is simple, just send a json like below to:
```
http://<server-name>/api/mastermind/newgame
```

```javascript
{
  "user": "Name"
}
```

You'll receive a response like that:

```javascript
{
  "colors": [
    "R",
    "B",
    "G",
    "Y",
    "O",
    "P",
    "C",
    "M"
  ],
  "codeLength": 8,
  "gamekey": "E6aFPF0t5EmVGcwnumjcPw==",
  "numGuesses": 0,
  "pastResults": [],
  "solved": false
}
```
-- **colors** The colors that you can chose to set the guess code.
-- **codeLength** The exact length you have to create the guess code.
-- **gamekey** The game identifier.
-- **numGuesses** The number that you tried guess the code.
-- **pastResults** All results of your guesses.
-- **solved** Game's solved.

The next step is send your guess for:
```
http://<server-name>/api/mastermind/guess
```
using:
```javascript
{
  "code": "GUESSCODE",
  "gamekey": "E6aFPF0t5EmVGcwnumjcPw=="
}
```


