using System;
using System.Collections.Generic;
using UnityEngine;

public class LD24 : MonoBehaviour
{
    public GameObject BrickHolder;

    public float playerAccerlation = 5.0f;
    public float playerSpeedMax = 100.0f;
    public float playerBreakSpeed = 7.0f;
    public float timeToKillMatch = 1.8f;

    private static List<Brick> bricks = new List<Brick>();
    private FContainer fContainerMain = new FContainer();
    private FContainer fContainerDeath = new FContainer();
    private FContainer fContainerBackground = new FContainer();
    private FSprite player;
    private float playerSpeedX = 0.0f;
    private float playerSpeedY = 0.0f;
    private static List<Brick> killingList = new List<Brick>();

    // Use this for initialization
    void Start()
    {
        FutileParams futileParams = new FutileParams(true, true, false, false);

        futileParams.AddResolutionLevel(1024, 1, 1, "");

        futileParams.origin = new Vector2(0, 0);
        futileParams.shouldLerpToNearestResolutionLevel = false;

        Futile.instance.Init(futileParams);
        Futile.screen.SignalResize += new System.Action<bool>(screen_SignalResize);
        screen_SignalResize(true);

        Futile.stage.AddChild(fContainerBackground);
        Futile.stage.AddChild(fContainerDeath);
        Futile.stage.AddChild(fContainerMain);

        Futile.atlasManager.LoadAtlas("Atlases/Atlas-1");

        FSprite sprite3 = new FSprite("Backgroundx4.png");
        sprite3.x = Futile.screen.halfWidth;
        sprite3.y = Futile.screen.halfHeight;
        sprite3.scale = 500;

        fContainerBackground.AddChild(sprite3);

        player = new FSprite("Brick.png");
        player.anchorX = 0;
        player.anchorY = 0;
        player.x = 25;
        player.y = 42;

        fContainerMain.AddChild(player);

        for (int x = 33; x + 40 < 1024; x += 44)
        {
            for (int y = 14 * 4; y + 10 < 768 - (14 * 1); y += 14)
            {
                AddBrick(x, y);
            }
        }

        // AddBrick(player.x, player.y + 15);

        // AddLogos();
    }

    private void AddBrick(float x, float y)
    {
        Brick brick = new Brick();
        brick.x = x;
        brick.y = y;

        bricks.Add(brick);
        fContainerMain.AddChild(brick);
    }
    private void AddBrick(int x, int y)
    {
        AddBrick((float)x, (float)y);
    }

    void screen_SignalResize(bool obj)
    {
        Futile.stage.scale = Math.Max(1.0f, Math.Min(Futile.screen.width / 1024, Futile.screen.height / 768));
        Debug.Log("Screen Resized!");
    }

    // Update is called once per frame
    void Update()
    {
        // Is player breaking
        if (Input.GetKey(KeyCode.Space))
        {
            if (playerSpeedX < 0)
            {
                playerSpeedX += playerBreakSpeed * Time.deltaTime;
                if (playerSpeedX > 0)
                {
                    playerSpeedX = 0;
                }
            }
            else
            {
                playerSpeedX -= playerBreakSpeed * Time.deltaTime;
                if (playerSpeedX < 0)
                {
                    playerSpeedX = 0;
                }
            }

            if (playerSpeedY < 0)
            {
                playerSpeedY += playerBreakSpeed * Time.deltaTime;
                if (playerSpeedY > 0)
                {
                    playerSpeedY = 0;
                }
            }
            else
            {
                playerSpeedY -= playerBreakSpeed * Time.deltaTime;
                if (playerSpeedY < 0)
                {
                    playerSpeedY = 0;
                }
            }
        }
        else
        {
            playerSpeedY += Input.GetAxis("Vertical") * playerAccerlation * Time.deltaTime;
            playerSpeedX += Input.GetAxis("Horizontal") * playerAccerlation * Time.deltaTime;
        }

        // Are we at the top speed for X?
        if (Math.Abs(playerSpeedX) > playerSpeedMax)
        {
            if (playerSpeedX < 0)
            {
                playerSpeedX = -1 * playerSpeedMax;
            }
            else
            {
                playerSpeedX = playerSpeedMax;
            }
        }

        // Are we at the top speed for Y?
        if (Math.Abs(playerSpeedY) > playerSpeedMax)
        {
            if (playerSpeedY < 0)
            {
                playerSpeedY = -1 * playerSpeedMax;
            }
            else
            {
                playerSpeedY = playerSpeedMax;
            }
        }

        // Did we bump into bottom or top of the screen?
        if (player.y + player.height > 768)
        {
            playerSpeedY = 0;
            player.y = 768 - player.height;
        }
        else if (player.y < 0)
        {
            playerSpeedY = 0;
            player.y = 0;
        }

        // Did we bump into the left or right of the screen?
        if (player.x + player.width > 1024)
        {
            playerSpeedX = 0;
            player.x = 1024 - player.width;
        }
        else if (player.x < 0)
        {
            playerSpeedX = 0;
            player.x = 0;
        }

        // Did we bump into a brick?
        List<Brick> stillTouching = new List<Brick>();
        for (int i = bricks.Count - 1; i >= 0; i--)
        {
            Brick brick = bricks[i];

            Rect rectBrick = brick.textureRect.CloneAndOffset(brick.x, brick.y);
            Rect rectPlayer = player.textureRect.CloneAndOffset(player.x, player.y);
            if (rectPlayer.CheckIntersect(rectBrick))
            {
                if (rectPlayer.y + player.height >= rectBrick.y
                    && rectPlayer.y + player.height <= rectBrick.y + brick.height)
                {
                    playerSpeedY = 0;
                    player.y = brick.y - player.height - 1;

                    if (brick.BeingKilled)
                    {
                        stillTouching.Add(brick);
                    }
                    else if (CheckMatch(brick))
                    {
                        BrickDeath(brick);
                    }
                }
            }
        }

        // Did we stop touching a brick on the killing list?
        for (int i = killingList.Count - 1; i >= 0; i--)
        {
            Brick brick = killingList[i];
            if (stillTouching.Contains(brick))
            {
                // Good, do nothing
            }
            else
            {
                // Bad, let it live
                brick.BeingKilled = false;
                brick.Tween.reverse();
                killingList.Remove(brick);
            }
        }

        // Move along X at our speed
        player.x += playerSpeedX;

        // Move along Y at our speed
        player.y += playerSpeedY;
    }

    private void BrickDeath(Brick brick)
    {
        brick.BeingKilled = true;

        fContainerDeath.AddChild(brick);

        TweenConfig tweenConfig = new TweenConfig()
            .floatProp("scale", 1.6f)
            .floatProp("alpha", 0.5f)
            .colorProp("color", new Color(1, 1, 1, 1))
            .onComplete(BrickDead);
        brick.Tween = Go.to(brick, timeToKillMatch, tweenConfig);
    }

    private static void BrickDead(AbstractTween tween)
    {
        Tween tweenReal = tween as Tween;
        Brick brick = tweenReal.target as Brick;

        // Make sure I'm still dieing
        if (brick.BeingKilled == true)
        {
            brick.RemoveFromContainer();
            bricks.Remove(brick);
        }
    }

    private void AddLogos()
    {

        FSprite sprite = new FSprite("Ludum-Dare.png");
        sprite.anchorX = 0;
        sprite.anchorY = 0;
        sprite.x = 10;
        sprite.y = 10;

        fContainerBackground.AddChild(sprite);

        FSprite sprite2 = new FSprite("MrPhilGamesLogo737.png");
        sprite2.anchorX = 0;
        sprite2.anchorY = 0;
        sprite2.x = 0;
        sprite2.y = Futile.screen.height - sprite2.height;

        fContainerBackground.AddChild(sprite2);
    }

    private bool CheckMatch(Brick brick)
    {
        return true;
    }
}
