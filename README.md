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

You'll receive another json like that:

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
