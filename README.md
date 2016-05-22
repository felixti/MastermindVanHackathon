# Mastermind VanHackathon API v1.0

### Overview

This API works in 2 different modes:

1. Single Player
2. Multiplayer

All the rules are implemented, so dont't worry about that.

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
- **colors** The colors that you can chose to set the guess code.
- **codeLength** The exact length you have to create the guess code.
- **gamekey** The game identifier.
- **numGuesses** The number that you tried guess the code.
- **pastResults** All results of your guesses.
- **solved** Game's solved.

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

After the play you'll receive the follow responses:

**When you don t solved the code:**

```javascript
{
  "codeLength": 8,
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
  "gamekey": "E6aFPF0t5EmVGcwnumjcPw==",
  "guess": "OPORCCGB",
  "numGuesses": 1,
  "pastResults": [
    {
      "exact": 0,
      "guess": "OPORCCGB",
      "near": 4
    }
  ],
  "result": {
    "exact": 0,
    "near": 4
  },
  "solved": false
}
```

**When you solve the code:**

```javascript
{
  "codeLength": 8,
  "furtherInstructions": "Solve the challenge to see this!",
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
  "gamekey": "y32slt4e5E6nW28f6hLbuw==",
  "guess": "GRGMMGCO",
  "numGuesses": 3,
  "pastResults": [
    {
      "exact": 0,
      "guess": "OPORCCGB",
      "near": 4
    },
    {
      "exact": 0,
      "guess": "OPORCCGB",
      "near": 4
    },
    {
      "exact": 8,
      "guess": "GRGMMGCO",
      "near": 0
    }
  ],
  "result": "You win!",
  "solved": true,
  "timeTaken": 54,
  "user": "felix"
}
```
Congratulations you win.

## Multiplayer Mode

In this mode will be necessary to send some new data to start the game, example below:
```
http://<server-name>/api/mastermindmultiplayer/join
```
```javascript
{
  name:"Felipe Felix", 
  role:"CodeBreaker", 
  roomId:""
}
```
You'll receive a response with the gamekey, roomid and message. The message says you need wait for second player.

```javascript
{
  "gamekey": "2rNym4JyF0uEQKWkYsB/Qw==",
  "roomdId": "e4938012-1478-480d-b53f-e2eea8134164",
  "message": "You are joined. Waiting for the second player!"
}
```
