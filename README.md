# ITU-CS-464-LAB-05-BSCS23051

**Course:** CS-464 Game Development, Information Technology University
**Lab 05:** Roll-a-Ball (Lecture 06)
**Name:** Muhammad Talha
**Roll number:** BSCS23051

![Screenshot](Docs/screenshot.png)

## About

This is the Roll-a-Ball game from Lecture 06, plus the three extra tasks.

You move the ball with WASD or the arrow keys and pick up the gold coins. Get 10 and you win, and the Restart button starts the game again.

I used the four scripts from the lecture:

- **PlayerController.cs** moves the ball with AddForce. I read input from the Input System's Move action instead of Input.GetAxis, because my project is set to the new Input System only.
- **Spawner.cs** spawns a gold coin every 1.5 s at a random spot, with a maximum of 5 at a time. It won't put one right next to the player.
- **Coin.cs** makes the coin spin. When the player touches it, it tells the GameManager and destroys itself.
- **GameManager.cs** keeps the score, updates the text, shows the win panel and handles Restart.

## Task 1: Red coin

When the game starts, the GameManager picks a random score from 1 to 9 (`Random.Range(1, 10)`). When the score gets to that number, one red coin spawns somewhere in the arena. Since the score only goes up by 1 each time, it can only hit that number once, so you only ever get one red coin.

I didn't go up to 10 because the game freezes when you win at 10, and then you'd never be able to collect the red coin.

The Console prints which score the red coin will come at, which makes it easier to test.

## Task 2: Blue coins

When you pick up the red coin, 50 blue coins spawn in a circle around the player, 5 m away. To get the positions I split 360° into 50 angles and used cos and sin for x and z. If the player is near a wall, some of the circle would end up outside the arena, so I clamp those coins to stay inside.

Blue coins don't add to the normal score.

## Task 3: BonusCoins text

There's a BonusCoins text in the top right corner that starts hidden. When the blue coins spawn it turns on and shows `BonusCoins: 0/50`, and it goes up by one for each blue coin you collect.

## Notes

- All three coin types use the same Coin script. It has a `kind` setting (Gold, Red or Blue) so the GameManager knows what was picked up.
- Unity 6.3 (6000.6.3f1), Universal 3D template
- Scene: `Assets/Scenes/SampleScene.unity`
- Prefabs: `Coin`, `RedCoin` and `BlueCoin` in `Assets/Prefabs`
