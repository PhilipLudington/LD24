using System;
using System.Collections.Generic;
using UnityEngine;

public class LD24 : MonoBehaviour
{
    private List<FSprite> bricks = new List<FSprite>();
    private FContainer fContainerMain = new FContainer();
    private FSprite player;
    private float playerSpeed = 40.0f;

    // Use this for initialization
    void Start()
    {
        FutileParams futileParams = new FutileParams(true, true, false, false);

        futileParams.AddResolutionLevel(1024, 1, 1, "");

        futileParams.origin = new Vector2(0, 0);
        futileParams.shouldLerpToNearestResolutionLevel = false;

        Futile.instance.Init(futileParams);
        //Futile.screen.SignalResize += new System.Action<bool>(screen_SignalResize);
        //screen_SignalResize(true);

        Futile.stage.AddChild(fContainerMain);

        Futile.atlasManager.LoadAtlas("Atlases/Atlas-1");

        FSprite sprite3 = new FSprite("Backgroundx4.png");
        sprite3.x = Futile.screen.halfWidth;
        sprite3.y = Futile.screen.halfHeight;
        sprite3.scale = 500;

        fContainerMain.AddChild(sprite3);

        //FSprite sprite = new FSprite("Ludum-Dare.png");
        //sprite.anchorX = 0;
        //sprite.anchorY = 0;
        //sprite.x = 10;
        //sprite.y = 10;

        //fContainerMain.AddChild(sprite);

        //FSprite sprite2 = new FSprite("MrPhilGamesLogo737.png");
        //sprite2.anchorX = 0;
        //sprite2.anchorY = 0;
        //sprite2.x = 0;
        //sprite2.y = Futile.screen.height - sprite2.height;

        //fContainerMain.AddChild(sprite2);

        player = new FSprite("Brick.png");
        player.anchorX = 0;
        player.anchorY = 0;
        player.x = 25;
        player.y = 42;

        fContainerMain.AddChild(player);

        for (int x = 25; x + 40 < 1024; x += 44)
        {
            for (int y = 14 * 4; y + 10 < 768 - (14 * 1); y += 14)
            {
                FSprite brick = new FSprite("Brick.png");
                brick.anchorX = 0;
                brick.anchorY = 0;
                brick.color = new Color(UnityEngine.Random.Range(0, 100) / 100.0f,
                    UnityEngine.Random.Range(0, 100) / 100.0f,
                    UnityEngine.Random.Range(0, 100) / 100.0f, 1);
                brick.x = x;
                brick.y = y;

                bricks.Add(brick);
                fContainerMain.AddChild(brick);
            }
        }
    }

    void screen_SignalResize(bool obj)
    {
        Futile.stage.scale = Math.Max(1.0f, Math.Min(Futile.screen.width / 1024, Futile.screen.height / 768));
        Debug.Log("Screen Resized!");
    }

    // Update is called once per frame
    void Update()
    {
        player.x += Input.GetAxis("Horizontal") * playerSpeed * Time.deltaTime;
        player.y += Input.GetAxis("Vertical") * playerSpeed * Time.deltaTime;
    }
}
